﻿using System.Reflection;

namespace Environment;

public class PathProvider : IPathProvider
{
    #region Properties

    #region DirectoryNames

    private static readonly string _configFolder = "Config";

    private static readonly string _logsFolder = "Logs";

    public static string DefaultMovieDestinationFolder => "Movies";

    public static string DefaultDownloadsDestinationFolder => "Downloads";

    public static string DefaultTvShowsDestinationFolder => "TvShows";

    public static string DefaultMusicDestinationFolder => "Music";

    public static string DefaultPhotosDestinationFolder => "Photos";

    public static string DefaultOtherDestinationFolder => "Other";

    public static string DefaultGamesDestinationFolder => "Games";

    #endregion

    #region FileNames

    public static string ConfigFileName => "PlexRipperSettings.json";

    public static string DatabaseName => "PlexRipperDB.db";

    #endregion

    public static string ConfigDirectory => Path.Combine(RootDirectory, _configFolder);

    public static string ConfigFileLocation => Path.Join(ConfigDirectory, ConfigFileName);

    public static string DatabaseBackupDirectory => Path.Combine(ConfigDirectory, "Database BackUp");

    public static string DatabasePath => Path.Combine(ConfigDirectory, DatabaseName);

    public static string LogsDirectory => Path.Combine(RootDirectory, _configFolder, _logsFolder);

    public static string RootDirectory
    {
        get
        {
            var devRootPath = EnvironmentExtensions.GetDevelopmentRootPath();
            if (devRootPath is not null)
                return devRootPath;

            switch (OsInfo.CurrentOS)
            {
                case OperatingSystemPlatform.Linux:
                case OperatingSystemPlatform.Osx:
                    return "/";
                case OperatingSystemPlatform.Windows:
                    return Path.GetPathRoot(Assembly.GetExecutingAssembly().Location) ?? @"C:\";
                default:
                    return "/";
            }
        }
    }

    #region Interface Implementations

    string IPathProvider.RootDirectory => RootDirectory;

    string IPathProvider.ConfigFileLocation => ConfigFileLocation;

    string IPathProvider.ConfigFileName => ConfigFileName;

    string IPathProvider.DatabaseBackupDirectory => DatabaseBackupDirectory;

    string IPathProvider.DatabaseName => DatabaseName;

    string IPathProvider.DatabasePath => DatabasePath;

    string IPathProvider.LogsDirectory => LogsDirectory;

    string IPathProvider.ConfigDirectory => ConfigDirectory;

    #endregion

    #endregion
}