using Application.Contracts;
using Data.Contracts;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace PlexRipper.Application;

public record GetPlexServerByIdEndpointRequest(int PlexServerId);

public class GetPlexServerByIdEndpointRequestValidator : Validator<GetPlexServerByIdEndpointRequest>
{
    public GetPlexServerByIdEndpointRequestValidator()
    {
        RuleFor(x => x.PlexServerId).GreaterThan(0);
    }
}

public class GetPlexServerByIdEndpoint : BaseCustomEndpoint<GetPlexServerByIdEndpointRequest, ResultDTO<PlexServerDTO>>
{
    private readonly IPlexRipperDbContext _dbContext;

    public override string EndpointPath => throw new NotImplementedException();

    public GetPlexServerByIdEndpoint(IPlexRipperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get(EndpointPath);
        AllowAnonymous();
        Description(x => x
            .Produces(StatusCodes.Status200OK, typeof(ResultDTO<PlexServerDTO>))
            .Produces(StatusCodes.Status404NotFound, typeof(ResultDTO))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ResultDTO)));
    }

    public override async Task HandleAsync(GetPlexServerByIdEndpointRequest req, CancellationToken ct)
    {
        var plexServer = await _dbContext.PlexServers.IncludeConnectionsWithStatus().GetAsync(req.PlexServerId, ct);

        if (plexServer is null)
        {
            await SendResult(ResultExtensions.EntityNotFound(nameof(PlexServer), req.PlexServerId), ct);
            return;
        }

        await SendResult(Result.Ok(plexServer.ToDTO()), ct);
    }
}