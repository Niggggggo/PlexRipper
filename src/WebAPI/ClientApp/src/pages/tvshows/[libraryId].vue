<template>
	<QPage>
		<MediaOverview
			:library-id="libraryId"
			:media-id="mediaId"
			:media-type="PlexMediaType.TvShow" />
	</QPage>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { PlexMediaType } from '@dto';
import { definePageMeta } from '#imports';

definePageMeta({
	scrollToTop: false,
});

const route = useRoute();

const mediaId = +route.params.tvShowId;
const libraryId = +route.params.libraryId;
</script>
