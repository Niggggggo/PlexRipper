<template>
	<q-toolbar class="media-overview-bar">
		<!--	Title	-->
		<q-toolbar-title>
			<QRow
				justify="start"
				align="center">
				<Transition
					appear
					enter-active-class="animated fadeInLeft"
					leave-active-class="animated fadeOutLeft">
					<QCol
						v-if="detailMode"
						cols="auto">
						<q-btn
							flat
							icon="mdi-arrow-left"
							size="xl"
							@click="$emit('back')" />
					</QCol>
				</Transition>
				<QCol cols="auto">
					<q-list class="no-background">
						<q-item>
							<q-item-section avatar>
								<QMediaTypeIcon
									class="mx-3"
									:size="36"
									:media-type="library?.type ?? PlexMediaType.None" />
							</q-item-section>
							<q-item-section>
								<q-item-label>
									{{ server ? serverStore.getServerName(server.id) : $t('general.commands.unknown') }}
									{{ $t('general.delimiter.dash') }}
									{{ library ? libraryStore.getLibraryName(library.id) : $t('general.commands.unknown') }}
								</q-item-label>
								<q-item-label
									v-if="library && !detailMode"
									caption>
									{{ libraryCountFormatted }}
									{{ $t('general.delimiter.dash') }}
									<QFileSize :size="library.mediaSize" />
								</q-item-label>
							</q-item-section>
						</q-item>
					</q-list>
				</QCol>
			</QRow>
		</q-toolbar-title>

		<!--	Download button	-->
		<VerticalButton
			v-if="mediaOverviewStore.showDownloadButton"
			icon="mdi-download"
			:label="$t('general.commands.download')"
			:height="barHeight"
			:width="verticalButtonWidth"
			@click="download" />

		<!--	Selection Dialog Button	-->
		<VerticalButton
			v-if="mediaOverviewStore.showSelectionButton"
			icon="mdi-select-marker"
			:label="$t('general.commands.selection')"
			:height="barHeight"
			:width="verticalButtonWidth"
			@click="$emit('selection-dialog')" />

		<!--	Refresh library button	-->
		<VerticalButton
			v-if="!detailMode"
			icon="mdi-refresh"
			:label="$t('general.commands.refresh')"
			:height="barHeight"
			cy="media-overview-refresh-library-btn"
			:width="verticalButtonWidth"
			@click="refreshLibrary" />

		<!--	View mode	-->
		<VerticalButton
			v-if="!detailMode"
			icon="mdi-eye"
			:label="$t('general.commands.view')"
			:height="barHeight"
			:width="verticalButtonWidth"
			cy="change-view-mode-btn">
			<q-menu
				anchor="bottom left"
				self="top left"
				auto-close>
				<q-list>
					<q-item
						v-for="(viewOption, i) in viewOptions"
						:key="i"
						clickable
						style="min-width: 200px"
						:data-cy="`view-mode-${viewOption.viewMode.toLowerCase()}-btn`"
						@click="changeView(viewOption.viewMode)">
						<!-- View mode options -->
						<q-item-section avatar>
							<q-avatar>
								<q-icon
									v-if="isSelected(viewOption.viewMode)"
									name="mdi-check" />
							</q-avatar>
						</q-item-section>
						<!--	Is selected icon	-->
						<q-item-section> {{ viewOption.label }}</q-item-section>
					</q-item>
				</q-list>
			</q-menu>
		</VerticalButton>
	</q-toolbar>
</template>

<script setup lang="ts">
import type { PlexLibraryDTO, PlexServerDTO } from '@dto';
import { PlexMediaType, ViewMode } from '@dto';
import { useLibraryStore, useMediaOverviewBarDownloadCommandBus, useMediaOverviewStore, useServerStore } from '#imports';

const libraryStore = useLibraryStore();
const serverStore = useServerStore();
const mediaOverviewStore = useMediaOverviewStore();
const downloadCommandBus = useMediaOverviewBarDownloadCommandBus();

interface IViewOptions {
	label: string;
	viewMode: ViewMode;
}

const props = defineProps<{
	server: PlexServerDTO | null;
	library: PlexLibraryDTO | null;
	detailMode?: boolean;
}>();

const emit = defineEmits<{
	(e: 'back' | 'selection-dialog'): void;
	(e: 'refresh-library', libraryId: number): void;
	(e: 'view-change', viewMode: ViewMode): void;
}>();

const barHeight = ref(85);
const verticalButtonWidth = ref(120);

const refreshLibrary = () => {
	emit('refresh-library', props.library?.id ?? -1);
};

const download = () => {
	downloadCommandBus.emit('download');
};

const changeView = (viewMode: ViewMode) => {
	emit('view-change', viewMode);
};

const isSelected = (viewMode: ViewMode) => {
	return mediaOverviewStore.getMediaViewMode === viewMode;
};

const libraryCountFormatted = computed(() => {
	if (props.library) {
		switch (props.library?.type) {
			case PlexMediaType.Movie:
				return `${props.library.count} Movies`;
			case PlexMediaType.TvShow:
				return `${props.library.count} TvShows - ${props.library.seasonCount} Seasons - ${props.library.episodeCount} Episodes`;
			default:
				return `Library type ${props.library?.type} is not supported in the media count`;
		}
	}
	return 'unknown media count';
});

const viewOptions = computed((): IViewOptions[] => {
	return [
		{
			label: 'Poster View',
			viewMode: ViewMode.Poster,
		},
		{
			label: 'Table View',
			viewMode: ViewMode.Table,
		},
	];
});
</script>
