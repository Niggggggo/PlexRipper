using Application.Contracts;
using FastEndpoints;
using FileSystem.Contracts;
using Microsoft.AspNetCore.Http;

namespace PlexRipper.Application;

public class GetFolderPathDirectoryRequest
{
    [QueryParam]
    public string Path { get; init; }
}

public class GetFolderPathDirectoryRequestValidator : Validator<GetFolderPathDirectoryRequest>
{
    public GetFolderPathDirectoryRequestValidator() { }
}

public class GetFolderPathDirectoryEndpoint : BaseEndpoint<GetFolderPathDirectoryRequest, FileSystemDTO>
{
    private readonly IFileSystem _fileSystem;

    public override string EndpointPath => ApiRoutes.FolderPathController + "/directory";

    public GetFolderPathDirectoryEndpoint(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override void Configure()
    {
        Get(EndpointPath);
        AllowAnonymous();
        Summary(x =>
        {
            x.Summary = "Get all the FolderPaths entities in the database";
        });
        Description(x =>
            x.Produces(StatusCodes.Status200OK, typeof(ResultDTO<FileSystemDTO>))
                .Produces(StatusCodes.Status400BadRequest, typeof(ResultDTO))
                .Produces(StatusCodes.Status500InternalServerError, typeof(ResultDTO))
        );
    }

    public override async Task HandleAsync(GetFolderPathDirectoryRequest req, CancellationToken ct)
    {
        var result = _fileSystem.LookupContents(req.Path, false, true);
        await SendFluentResult(result, x => x.ToDTO(), ct);
    }
}
