﻿using Application.Contracts;
using AutoMapper;
using Data.Contracts;
using Logging.Interface;
using Microsoft.AspNetCore.Mvc;
using PlexRipper.Application;
using PlexRipper.WebAPI.Common.Extensions;

namespace PlexRipper.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlexMediaController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IPlexRipperDbContext _dbContext;

    public PlexMediaController(
        ILog log,
        IMediator mediator,
        IPlexRipperDbContext dbContext,
        IMapper mapper,
        INotificationsService notificationsService) : base(log, mapper, notificationsService)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    // GET api/<PlexMedia>/library/5
    [HttpGet("library/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDTO<List<PlexMediaSlimDTO>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultDTO))]
    public async Task<IActionResult> GetLibraryMedia(int id, [FromQuery] int page, [FromQuery] int size, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return BadRequest(id, nameof(id));

        var result = await _mediator.Send(new GetPlexMediaDataByLibraryIdQuery(id, page, size), cancellationToken);
        if (result.IsFailed)
            return ToActionResult(result.ToResult());

        var dtos = _mapper.Map<List<PlexMediaSlimDTO>>(result.Value).SetIndex();
        return Ok(Result.Ok(dtos));
    }

    // GET api/<PlexMedia>/5
    [HttpGet("thumb")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status408RequestTimeout, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResultDTO))]
    public async Task<IActionResult> GetThumb(
        int plexMediaId,
        PlexMediaType plexMediaType,
        int width,
        int height,
        CancellationToken cancellationToken = default)
    {
        if (plexMediaId == 0)
            return BadRequestInvalidId();

        var result = await _mediator.Send(new GetThumbnailImageQuery(plexMediaId, plexMediaType, width, height), cancellationToken);

        if (result.IsSuccess)
        {
            if (result.Value.Any())
                return File(result.Value, "image/jpeg");

            return NoContent();
        }

        return ToActionResult(result.ToResult());
    }
}