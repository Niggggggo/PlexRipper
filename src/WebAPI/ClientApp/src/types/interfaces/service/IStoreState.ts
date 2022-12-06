import {
	DownloadTaskCreationProgress,
	DownloadTaskDTO,
	FileMergeProgress,
	FolderPathDTO,
	InspectServerProgressDTO,
	LibraryProgress,
	NotificationDTO,
	PlexAccountDTO,
	PlexLibraryDTO,
	PlexServerConnectionDTO,
	PlexServerDTO,
	ServerDownloadProgressDTO,
	SettingsModelDTO,
	SyncServerProgress,
} from '@dto/mainApi';
import IObjectUrl from '@interfaces/IObjectUrl';
import IAlert from '@interfaces/IAlert';
import IAppConfig from '@class/IAppConfig';

export default interface IStoreState extends SettingsModelDTO {
	pageReady: boolean;
	config: IAppConfig;
	accounts: PlexAccountDTO[];
	servers: PlexServerDTO[];
	serverConnections: PlexServerConnectionDTO[];
	libraries: PlexLibraryDTO[];
	serverDownloads: ServerDownloadProgressDTO[];
	notifications: NotificationDTO[];
	folderPaths: FolderPathDTO[];
	alerts: IAlert[];
	mediaUrls: IObjectUrl[];
	helpIdDialog: string;
	downloadTaskUpdateList: DownloadTaskDTO[];
	// Progress Service
	libraryProgress: LibraryProgress[];
	fileMergeProgressList: FileMergeProgress[];
	inspectServerProgress: InspectServerProgressDTO[];
	syncServerProgress: SyncServerProgress[];
	downloadTaskCreationProgress: DownloadTaskCreationProgress;
}
