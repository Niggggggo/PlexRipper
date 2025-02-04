﻿namespace PlexRipper.Domain;

public class PlexRole : BaseEntity
{
    public required string Tag { get; init; }

    public List<PlexMovieRole> PlexMovieRoles { get; init; } = new();
}
