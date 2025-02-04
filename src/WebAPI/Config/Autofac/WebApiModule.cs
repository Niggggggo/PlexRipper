﻿using Application.Contracts;
using Autofac;

namespace PlexRipper.WebAPI;

public class WebApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Boot>().As<IBoot>().SingleInstance();

        builder.Register(c => c.Resolve<IHttpClientFactory>().CreateClient()).As<HttpClient>().InstancePerDependency();

        // This needs to be registered in order to fire Boot on Application startup
        builder.RegisterType<Boot>().As<IHostLifetime>().SingleInstance();

        // SignalR
        builder.RegisterType<SignalRService>().As<ISignalRService>();
        builder.RegisterType<ProgressHub>().ExternallyOwned();
        builder.RegisterType<NotificationHub>().ExternallyOwned();
    }
}
