using System.Reflection;
using System.Text.Json.Serialization;
using Environment;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using PlexRipper.Application;
using PlexRipper.Data;
using PlexRipper.Domain.Config;
using PlexRipper.WebAPI.SignalR.Hubs;

namespace PlexRipper.WebAPI;

public class Startup
{
    public static readonly string CORSConfiguration = "CORS_Configuration";

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
    {
        // Set CORS Configuration
        services.AddCors(options =>
        {
            options.AddPolicy(
                CORSConfiguration,
                builder =>
                {
                    // TODO CORS disabled, otherwise its not working when deployed in a docker container
                    // Solution?
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()

                        // The combo all origin is allowed with allow credentials is needed to make SignalR work from the client.
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials();
                });
        });

        services.AddOptions();

        services.AddAuthorization();

        // Setup FastEndpoints
        services.AddFastEndpoints(options =>
        {
            options.DisableAutoDiscovery = true;
            options.Assemblies = new[]
            {
                Assembly.GetAssembly(typeof(BaseEndpoint<,>)),
            };
        });

        if (!EnvironmentExtensions.IsIntegrationTestMode())
        {
            // Used to deploy the front-end Nuxt client
            if (env.IsProduction())
                services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");

            if (env.IsDevelopment())
                services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp");

            // Setup SignalR
            services
                .AddSignalR()
                .AddJsonProtocol(options => options.PayloadSerializerOptions = DefaultJsonSerializerOptions.ConfigBase);

            services.SwaggerDocument(options =>
            {
                // https://fast-endpoints.com/docs/swagger-support#swagger-operation-tags
                options.AutoTagPathSegmentIndex = 2;

                // https://fast-endpoints.com/docs/swagger-support#short-schema-names
                options.ShortSchemaNames = true;

                // https://fast-endpoints.com/docs/swagger-support#removing-empty-schema
                options.RemoveEmptyRequestSchema = true;

                options.FlattenSchema = true;
                options.SerializerSettings = serializerOptions =>
                {
                    var config = DefaultJsonSerializerOptions.ConfigBase;
                    serializerOptions.PropertyNameCaseInsensitive = config.PropertyNameCaseInsensitive;
                    serializerOptions.PropertyNamingPolicy = config.PropertyNamingPolicy;
                    serializerOptions.DefaultIgnoreCondition = config.DefaultIgnoreCondition;

                    // This will ensure that Enums are not exported as integers but as string values in the Swagger UI
                    serializerOptions.Converters.Add(new JsonStringEnumConverter());
                };

                // options.ExcludeNonFastEndpoints = true;
                options.DocumentSettings = s =>
                {
                    s.Title = "[FastEndpoints] PlexRipper Swagger Internal API";
                    s.Version = "v1";
                    s.MarkNonNullablePropsAsRequired();
                    s.RequireParametersWithoutDefault = true;
                    s.DocumentProcessors.Add(new NSwagAddExtraTypes());
                };
            });
        }

        services.AddHttpClient();

        // Removing all registered IHttpMessageHandlerBuilderFilter instances to disable built-in HttpClient logging
        services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to configure.</param>
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseCors(CORSConfiguration);

        app.UseAuthorization();

        if (!EnvironmentExtensions.IsIntegrationTestMode())
        {
            // SignalR configuration
            app.MapHub<ProgressHub>("/progress");
            app.MapHub<NotificationHub>("/notifications");
        }

        // Setup FastEndpoints
        app.UseFastEndpoints(c =>
        {
            c.Errors.ResponseBuilder = (failures, ctx, _) =>
            {
                var result = ResultExtensions.Create400BadRequestResult($"Bad request: {ctx.Request.GetDisplayUrl()}");
                var errors = failures.GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Select(m => m.ErrorMessage).ToArray());
                foreach (var reason in errors)
                    result.Errors[0].Metadata.Add(reason.Key, reason.Value);
                return result;
            };
        });

        // Setup FastEndpoints Swagger
        app.UseSwaggerGen();

        if (!EnvironmentExtensions.IsIntegrationTestMode() && env.IsProduction())
        {
            // Used to deploy the front-end Nuxt client
            app.UseSpaStaticFiles();
            app.UseSpa(spa => { spa.Options.SourcePath = "ClientApp"; });
        }
    }

    public static void ConfigureDatabase()
    {
        var dbContext = new PlexRipperDbContext(new PathProvider());
        dbContext.Setup();
    }
}