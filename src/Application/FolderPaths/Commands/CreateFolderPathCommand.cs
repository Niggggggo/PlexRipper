﻿namespace PlexRipper.Application
{
    public class CreateFolderPathCommand: IRequest<Result<int>>
    {
        public FolderPath FolderPath { get; }

        public CreateFolderPathCommand(FolderPath folderPath)
        {
            FolderPath = folderPath;
        }
    }

}