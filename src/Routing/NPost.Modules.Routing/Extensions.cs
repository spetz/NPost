using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Routing.Controllers;
using NPost.Modules.Routing.Services;
using NPost.Shared;

[assembly: InternalsVisibleTo("NPost")]
namespace NPost.Modules.Routing
{
    internal static class Extensions
    {
        public static Module AddRoutingModule(this IServiceCollection services)
        {
            services.AddScoped<IRoutingService, DummyRoutingService>();
            
            return new Module(typeof(RoutingApiController).Assembly, "NPost.Modules.Routing");
        }

        public static IApplicationBuilder UseRoutingModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}