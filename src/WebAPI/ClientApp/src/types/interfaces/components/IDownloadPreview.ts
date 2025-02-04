import type { PlexMediaType } from '@dto';

export default interface IDownloadPreview {
	id: number;
	title: string;
	type: PlexMediaType;
	size: number;
	children: IDownloadPreview[];
}
