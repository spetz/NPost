using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Deliveries.Application.Services;
using NPost.Modules.Deliveries.Core.Repositories;
using NPost.Modules.Deliveries.Infrastructure.InMemory.Repositories;
using NPost.Modules.Deliveries.Infrastructure.Services;

[assembly: InternalsVisibleTo("NPost")]
[assembly: InternalsVisibleTo("NPost.Modules.Deliveries")]
namespace NPost.Modules.Deliveries.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
            services.AddTransient<IRoutingServiceClient, RoutingServiceClient>();
            services.AddSingleton<IDeliveriesRepository, InMemoryDeliveriesRepository>();
            services.AddSingleton<IParcelsRepository, InMemoryParcelsRepository>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
    }
}