using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Deliveries.Application;
using NPost.Modules.Deliveries.Controllers;
using NPost.Modules.Deliveries.Infrastructure;
using NPost.Shared;

[assembly: InternalsVisibleTo("NPost")]
namespace NPost.Modules.Deliveries
{
    internal static class Extensions
    {
        public static Module AddDeliveriesModule(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();

            return new Module(typeof(DeliveriesApiController).Assembly, "NPost.Modules.Deliveries");
        }

        public static IApplicationBuilder UseDeliveriesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}