﻿namespace PlexRipper.Application;

public class GetPlexServersByIdsQuery : IRequest<Result<List<PlexServer>>>
{
    public GetPlexServersByIdsQuery(List<int> ids, bool includeLibraries = false)
    {
        Ids = ids;
        IncludeLibraries = includeLibraries;
    }

    public List<int> Ids { get; }

    public bool IncludeLibraries { get; }
}