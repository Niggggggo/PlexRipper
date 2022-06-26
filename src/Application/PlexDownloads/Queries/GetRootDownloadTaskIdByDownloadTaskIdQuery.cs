﻿namespace PlexRipper.Application
{
    public class GetRootDownloadTaskIdByDownloadTaskIdQuery : IRequest<Result<int>>
    {
        public GetRootDownloadTaskIdByDownloadTaskIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}