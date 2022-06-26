﻿namespace PlexRipper.Application
{
    public class AddDownloadWorkerLogsCommand : IRequest<Result>
    {
        public IList<DownloadWorkerLog> DownloadWorkerLogs { get; }

        public AddDownloadWorkerLogsCommand(IList<DownloadWorkerLog> downloadWorkerLogs)
        {
            DownloadWorkerLogs = downloadWorkerLogs;
        }
    }
}