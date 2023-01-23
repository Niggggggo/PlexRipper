﻿using PlexRipper.Application;
using PlexRipper.Application.BackgroundServices;

namespace PlexRipper.BaseTests;

public class MockSignalRService : ISignalRService
{
    public void SendLibraryProgressUpdate(LibraryProgress libraryProgress) { }

    public void SendLibraryProgressUpdate(int id, int received, int total, bool isRefreshing = true) { }

    public Task SendDownloadTaskCreationProgressUpdate(int current, int total)
    {
        return Task.CompletedTask;
    }

    public void SendDownloadTaskUpdate(DownloadTask downloadTask) { }

    public Task SendFileMergeProgressUpdate(FileMergeProgress fileMergeProgress)
    {
        return Task.CompletedTask;
    }

    public Task SendNotification(Notification notification)
    {
        return Task.CompletedTask;
    }

    public Task SendServerInspectStatusProgress(InspectServerProgress progress)
    {
        return Task.CompletedTask;
    }

    public void SendServerSyncProgressUpdate(SyncServerProgress syncServerProgress) { }

    public Task SendDownloadProgressUpdate(int plexServerId, List<DownloadTask> downloadTasks)
    {
        return Task.CompletedTask;
    }

    public Task SendServerConnectionCheckStatusProgress(ServerConnectionCheckStatusProgress progress)
    {
        return Task.CompletedTask;
    }

    public Task SendJobStatusUpdate(JobStatusUpdate jobStatusUpdate)
    {
        return Task.CompletedTask;
    }
}