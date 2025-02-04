import {
	type MockConfig,
	generatePlexServers,
	generateResultDTO,
	checkConfig,
	generateServerDownloadProgress,
	generatePlexMedias,
	generateDownloadTask,
	generatePlexLibrariesFromPlexServers, generatePlexServerConnections,
} from '@mock';
import { generateSettingsModel } from '@factories/settings-factory';
import { generatePlexAccounts } from '@factories/plex-account-factory';
import type {
	PlexAccountDTO,
	PlexLibraryDTO,
	PlexMediaSlimDTO,
	PlexServerConnectionDTO,
	PlexServerDTO,
	ServerDownloadProgressDTO,
	SettingsModelDTO,
	DownloadTaskDTO,
} from '@dto';
import { PlexMediaType } from '@dto';
import {
	DownloadPaths,
	FolderPathPaths,
	NotificationPaths,
	PlexAccountPaths,
	PlexLibraryPaths,
	PlexMediaPaths,
	PlexServerConnectionPaths,
	PlexServerPaths,
	SettingsPaths,
} from '@api/api-paths';
import Convert from '@class/Convert';

export interface IBasePageSetupResult {
	plexServers: PlexServerDTO[];
	plexServerConnections: PlexServerConnectionDTO[];
	plexLibraries: PlexLibraryDTO[];
	plexAccounts: PlexAccountDTO[];
	serverDownloadProgress: ServerDownloadProgressDTO[];
	detailDownloadTasks: DownloadTaskDTO[];
	mediaData: {
		libraryId: number;
		media: PlexMediaSlimDTO[];
	}[];
	settings: SettingsModelDTO;
	config: MockConfig;
}

export function basePageSetup(config: Partial<MockConfig> = {}): Cypress.Chainable<IBasePageSetupResult> {
	const validConfig = checkConfig(config);
	const result: IBasePageSetupResult = {
		config: validConfig,
		mediaData: [],
		plexAccounts: [],
		plexLibraries: [],
		plexServerConnections: [],
		serverDownloadProgress: [],
		settings: {} as SettingsModelDTO,
		plexServers: [],
		detailDownloadTasks: [],
	};

	// PlexServers call
	result.plexServers = generatePlexServers({ config });
	cy.intercept('GET', PlexServerPaths.getAllPlexServersEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.plexServers),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> plexServers', result.plexServers);
		}
	});

	// PlexServerConnections call
	for (const plexServer of result.plexServers) {
		result.plexServerConnections.push(...generatePlexServerConnections({ plexServerId: plexServer.id, config }));
	}
	cy.intercept('GET', PlexServerConnectionPaths.getAllPlexServerConnectionsEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.plexServerConnections),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> plexServerConnections', result.plexServerConnections);
		}
	});

	// PlexLibraries call
	result.plexLibraries = generatePlexLibrariesFromPlexServers({ plexServers: result.plexServers, config });

	cy.intercept('GET', PlexLibraryPaths.getAllPlexLibrariesEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.plexLibraries),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> plexLibraries', result.plexLibraries);
		}
	});

	// Detail call for every library
	for (const library of result.plexLibraries) {
		cy.intercept('GET', PlexLibraryPaths.getPlexLibraryByIdEndpoint(library.id), {
			statusCode: 200,
			body: generateResultDTO(library),
		});
	}

	// PlexAccount call
	result.plexAccounts = generatePlexAccounts({ config, plexServers: result.plexServers, plexLibraries: result.plexLibraries });
	cy.intercept('GET', PlexAccountPaths.getAllPlexAccountsEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.plexAccounts),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> plexAccounts', result.plexAccounts);
		}
	});

	// DownloadTasks call
	result.serverDownloadProgress = result.plexServers
		.map((x) =>
			generateServerDownloadProgress({
				plexServerId: x.id,
				plexLibraryId: -1,
				config,
			}),
		)
		.flat();
	cy.intercept('GET', DownloadPaths.getAllDownloadTasksEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.serverDownloadProgress),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> downloadTasks', result.serverDownloadProgress);
		}
	});

	// DownloadDetails call
	if (config.setDownloadDetails) {
		for (const serverDownload of result.serverDownloadProgress) {
			const downloadTasks = result.serverDownloadProgress.flatMap((x) => x.downloads);
			for (const downloadTask of downloadTasks) {
				const generatedDownloadTask = generateDownloadTask({
					config,
					id: downloadTask.id,
					plexLibraryId: 1,
					plexServerId: serverDownload.id,
					type: Convert.toDownloadTaskType(downloadTask.mediaType),
					// partial: downloadTask,
				});
				result.detailDownloadTasks.push(generatedDownloadTask);
				cy.intercept('GET', DownloadPaths.getDownloadTaskByGuidEndpoint(downloadTask.id), {
					statusCode: 200,
					body: generateResultDTO(generatedDownloadTask),
				});
			}
		}
	}

	// Settings call
	result.settings = generateSettingsModel({ plexServers: result.plexServers, config });
	cy.intercept('GET', SettingsPaths.getUserSettingsEndpoint(), {
		statusCode: 200,
		body: generateResultDTO(result.settings),
	}).then(() => {
		if (validConfig.debugDisplayData) {
			cy.log('BasePageSetup -> settings', result.settings);
		}
	});

	// Generate library media page data
	for (const library of result.plexLibraries) {
		if (library.type !== PlexMediaType.Movie) {
			continue;
		}
		const mediaList = generatePlexMedias({
			plexLibraryId: library.id,
			plexServerId: library.plexServerId,
			type: library.type,
			config,
		});

		result.mediaData.push({
			libraryId: library.id,
			media: mediaList,
		});

		// Intercept the Library media call
		cy.intercept(
			'GET',
			PlexMediaPaths.getMediaDetailByIdEndpoint(library.id, {
				type: library.type,
			}),
			{
				statusCode: 200,
				body: generateResultDTO(mediaList),
			},
		);
	}

	cy.intercept('GET', FolderPathPaths.getAllFolderPathsEndpoint(), {
		statusCode: 200,
		body: generateResultDTO([]),
	});

	cy.intercept('GET', NotificationPaths.getAllNotificationsEndpoint(), {
		statusCode: 200,
		body: generateResultDTO([]),
	});

	cy.intercept('GET', '/progress', {
		statusCode: 200,
		body: {},
	});

	cy.intercept('GET', '/notifications', {
		statusCode: 200,
		body: {},
	});

	// Correct library data
	for (const library of result.plexLibraries) {
		const mediaList = result.mediaData.find((x) => x.libraryId === library.id)?.media ?? [];
		if (mediaList.length) {
			library.mediaSize = mediaList.reduce((acc, x) => acc + x.mediaSize, 0);
			library.count = mediaList.length;
		}
	}

	return cy.wrap(result);
}

export function route(path: string) {
	return Cypress.env('BASE_URL') + path;
}
