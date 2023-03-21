import { fileURLToPath } from 'url';
import { defineNuxtConfig } from 'nuxt/config';

const silenceSomeSassDeprecationWarnings = {
	verbose: true,
	logger: {
		warn(message, options) {
			const { stderr } = process;
			const span = options.span ?? undefined;
			const stack = (options.stack === 'null' ? undefined : options.stack) ?? undefined;

			if (options.deprecation) {
				if (message.startsWith('Using / for division outside of calc() is deprecated')) {
					// silences above deprecation warning
					return;
				}
				stderr.write('DEPRECATION ');
			}
			stderr.write(`WARNING: ${message}\n`);

			if (span !== undefined) {
				// output the snippet that is causing this warning
				stderr.write(`\n"${span.text}"\n`);
			}

			if (stack !== undefined) {
				// indent each line of the stack
				stderr.write(`    ${stack.toString().trimEnd().replace(/\n/gm, '\n    ')}\n`);
			}

			stderr.write('\n');
		},
	},
};

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
	ssr: false,
	srcDir: 'src',
	devServer: {
		port: 3001,
	},
	runtimeConfig: {
		// Private config that is only available on the server
		apiSecret: '123',
		// Config within public will be also exposed to the client
		public: {
			nodeEnv: process.env.NODE_ENV || 'development',
			version: process.env.npm_package_version || '?',
			baseURL: process.env.BASE_URL || 'http://localhost:5000',
			baseApiPath: '/api',
		},
	},
	modules: [
		// Doc: https://github.com/Maiquu/nuxt-quasar
		'nuxt-quasar-ui',
		'@vueuse/nuxt',
		'@nuxt/devtools',
		// Doc: https://i18n.nuxtjs.org/
		'@nuxtjs/i18n',
		'@vue-macros/nuxt',
	],
	quasar: {
		// Plugins: https://quasar.dev/quasar-plugins
		plugins: ['Loading'],
		// Truthy values requires `sass@1.32.12`.
		sassVariables: 'src/assets/scss/_variables.scss',

		// Requires `@quasar/extras` package
		extras: {
			// string | null: Auto-import roboto font. https://quasar.dev/style/typography#default-font
			font: 'roboto-font',
			// string[]: Auto-import webfont icons. Usage: https://quasar.dev/vue-components/icon#webfont-usage
			fontIcons: ['mdi-v6'],
			// string[]: Auto-import svg icon collections. Usage: https://quasar.dev/vue-components/icon#svg-usage
			svgIcons: [],
			// string[]: Auto-import animations from 'animate.css'. Usage: https://quasar.dev/options/animations#usage
			animations: [],
		},
	},
	typescript: {
		// Doc: https://typescript.nuxtjs.org/guide/setup.html#configuration
		// Packages,  @types/node, vue-tsc and typescript are required
		typeCheck: true,
		strict: true,
	},
	macros: {
		// Enabled betterDefine to allow importing interfaces into defineProps
		betterDefine: true,
	},
	i18n: {
		lazy: true,
		langDir: './lang/',
		defaultLocale: 'en-US',
		locales: [
			{ text: 'English', code: 'en-US', iso: 'en-US', file: 'en-US.json' },
			{ text: 'Français', code: 'fr-FR', iso: 'fr-FR', file: 'fr-FR.json' },
			{ text: 'Deutsch', code: 'de-DE', iso: 'de-DE', file: 'de-DE.json' },
		],
		vueI18n: {
			fallbackLocale: 'en-US',
		},
		strategy: 'no_prefix',
	},
	/*
	 ** Global CSS: https://nuxt.com/docs/api/configuration/nuxt-config#css
	 */
	// css: ['quasar/src/css/index.sass', '@/assets/scss/style.scss'],
	alias: {
		// Doc: https://nuxt.com/docs/api/configuration/nuxt-config#alias
		'@class': fileURLToPath(new URL('./src/types/class/', import.meta.url)),
		'@dto': fileURLToPath(new URL('./src/types/dto/', import.meta.url)),
		'@api': fileURLToPath(new URL('./src/types/api/', import.meta.url)),
		'@const': fileURLToPath(new URL('./src/types/const/', import.meta.url)),
		'@buttons': fileURLToPath(new URL('./src/components/Buttons/', import.meta.url)),
		'@api-urls': fileURLToPath(new URL('./src/types/const/api-urls.ts', import.meta.url)),
		'@fixtures': fileURLToPath(new URL('../cypress/fixtures/', import.meta.url)),
		'@services-test-base': fileURLToPath(new URL('./src/tests/services/_base/base.ts', import.meta.url)),
		'@lib': fileURLToPath(new URL('./src/types/lib/', import.meta.url)),
		'@service': fileURLToPath(new URL('./src/service/', import.meta.url)),
		'@img': fileURLToPath(new URL('./src/assets/img/', import.meta.url)),
		'@enums': fileURLToPath(new URL('./src/types/enums/', import.meta.url)),
		'@mock': fileURLToPath(new URL('./src/mock-data/', import.meta.url)),
		'@interfaces': fileURLToPath(new URL('./src/types/interfaces/', import.meta.url)),
		'@components': fileURLToPath(new URL('./src/components/', import.meta.url)),
		'@overviews': fileURLToPath(new URL('./src/components/overviews/', import.meta.url)),
		'@mediaOverview': fileURLToPath(new URL('./src/components/MediaOverview/', import.meta.url)),
		'@vTreeViewTable': fileURLToPath(new URL('./src/components/General/VTreeViewTable/', import.meta.url)),
	},
	/*
	 ** Auto-import components
	 *  Doc: https://github.com/nuxt/components
	 */
	components: {
		loader: true,
		dirs: [
			// Components directory
			{
				path: './components',
				pathPrefix: false,
				extensions: ['vue'],
			},
			// Pages directory
			{
				path: './pages',
				pathPrefix: false,
				extensions: ['vue'],
			},
		],
	},
	vite: {
		css: {
			preprocessorOptions: {
				scss: {
					...silenceSomeSassDeprecationWarnings,

					// Make variables available everywhere
					// Doc: https://nuxt.com/docs/getting-started/assets#global-styles-imports
					additionalData: '@use "@/assets/scss/_variables.scss" as *;',
				},
				sass: {
					...silenceSomeSassDeprecationWarnings,
				},
			},
		},
	},
	/*
	 ** Doc: https://nuxtjs.org/docs/configuration-glossary/configuration-telemetry
	 */
	telemetry: false,
	/*
	 ** Customize the progress-bar color
	 */
	// loading: false, // TODO Maybe better to re-enable based on how it looks
});
