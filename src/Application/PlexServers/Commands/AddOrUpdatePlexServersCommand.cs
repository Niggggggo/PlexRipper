﻿namespace PlexRipper.Application;

public class AddOrUpdatePlexServersCommand : IRequest<Result>
{
    public PlexAccount PlexAccount { get; }

    public List<PlexServer> PlexServers { get; }

    public AddOrUpdatePlexServersCommand(PlexAccount plexAccount, List<PlexServer> plexServers)
    {
        PlexAccount = plexAccount;
        PlexServers = plexServers;
    }
}