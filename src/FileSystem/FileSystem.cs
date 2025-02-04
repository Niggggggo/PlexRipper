﻿using Environment;
using FileSystem.Contracts;
using Logging.Interface;

namespace PlexRipper.FileSystem;

public class FileSystem : IFileSystem
{
    #region Fields

    private readonly ILog _log;
    private readonly IPathProvider _pathProvider;

    private readonly System.IO.Abstractions.IFileSystem _abstractedFileSystem;

    private readonly IDiskProvider _diskProvider;

    private readonly IDirectorySystem _directorySystem;

    #endregion

    #region Constructor

    public FileSystem(
        ILog log,
        IPathProvider pathProvider,
        System.IO.Abstractions.IFileSystem abstractedFileSystem,
        IDiskProvider diskProvider,
        IDirectorySystem directorySystem
    )
    {
        _log = log;
        _pathProvider = pathProvider;
        _abstractedFileSystem = abstractedFileSystem;
        _diskProvider = diskProvider;
        _directorySystem = directorySystem;
    }

    #endregion

    #region Public Methods

    public bool FileExists(string path) => !string.IsNullOrEmpty(path) && _abstractedFileSystem.File.Exists(path);

    public Result<Stream> Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
        try
        {
            return Result.Ok(_abstractedFileSystem.File.Open(path, mode, access, share));
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    public Result<Stream> Create(string path, int bufferSize, FileOptions options)
    {
        try
        {
            var createResult = _abstractedFileSystem.File.Create(path, bufferSize, options);
            return Result.Ok(createResult);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    public Result<string> FileReadAllText(string path)
    {
        if (string.IsNullOrEmpty(path))
            return Result.Fail($"path is empty: \"{path}\"");

        try
        {
            var text = _abstractedFileSystem.File.ReadAllText(path);
            return Result.Ok(text);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }
    }

    public Result FileWriteAllText(string path, string text)
    {
        if (string.IsNullOrEmpty(path))
            return Result.Fail($"path is empty: \"{path}\"");

        try
        {
            _abstractedFileSystem.File.WriteAllText(path, text);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }
    }

    public Result DeleteFile(string filePath)
    {
        try
        {
            _abstractedFileSystem.File.Delete(filePath);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    public Result<FileSystemResult> LookupContents(
        string query,
        bool includeFiles,
        bool allowFoldersWithoutTrailingSlashes
    )
    {
        _log.Debug("Looking up path: {Query}", query);

        var defaultResult = new FileSystemResult
        {
            Directories = GetDrives(),
            Files = [],
            Parent = "",
            Current = null,
        };

        // If path is invalid return root file system
        if (string.IsNullOrWhiteSpace(query))
            return Result.Ok(defaultResult);

        var directoryExistsResult = _directorySystem.Exists(query);
        if (directoryExistsResult.IsFailed || !directoryExistsResult.Value)
            return Result.Ok(defaultResult);

        if (allowFoldersWithoutTrailingSlashes)
            return GetFileSystemResults(query, includeFiles);

        var lastSeparatorIndex = query.LastIndexOf(_abstractedFileSystem.Path.DirectorySeparatorChar);
        var path = query.Substring(0, lastSeparatorIndex + 1);

        if (lastSeparatorIndex != -1)
            return GetFileSystemResults(path, includeFiles);

        return Result.Ok(defaultResult);
    }

    public string ToAbsolutePath(string relativePath) =>
        _abstractedFileSystem.Path.GetFullPath(
            _abstractedFileSystem.Path.Combine(_pathProvider.RootDirectory, relativePath)
        );

    public Result FileMove(string sourceFileName, string destFileName, bool overwrite = true)
    {
        try
        {
            _abstractedFileSystem.File.Move(sourceFileName, destFileName, overwrite);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    #endregion

    #region Private Methods

    private List<FileSystemModel> GetDrives()
    {
        return _diskProvider
            .GetAllMounts()
            .Select(d => new FileSystemModel
            {
                Type = FileSystemEntityType.Drive,
                Name = _diskProvider.GetVolumeName(d),
                Path = d.RootDirectory.FullName,
                LastModified = d.RootDirectory.LastWriteTimeUtc,
                Extension = string.Empty,
                Size = d.TotalSize,
                HasReadPermission = d.CanRead(),
                HasWritePermission = d.CanWrite(),
            })
            .ToList();
    }

    private Result<FileSystemResult> GetFileSystemResults(string path, bool includeFiles)
    {
        try
        {
            var directoriesResult = _diskProvider.GetDirectories(path);
            if (directoriesResult.IsFailed)
                return directoriesResult.ToResult();

            if (includeFiles)
            {
                var filesResult = _diskProvider.GetFiles(path);
                if (filesResult.IsFailed)
                    return filesResult.ToResult();

                return Result.Ok(
                    new FileSystemResult()
                    {
                        Parent = _diskProvider.GetParent(path),
                        Directories = directoriesResult.Value,
                        Files = filesResult.Value,
                        Current = new DirectoryInfo(path).ToModel(),
                    }
                );
            }

            return Result.Ok(
                new FileSystemResult()
                {
                    Parent = _diskProvider.GetParent(path),
                    Directories = directoriesResult.Value,
                    Files = [],
                    Current = new DirectoryInfo(path).ToModel(),
                }
            );
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    #endregion
}
