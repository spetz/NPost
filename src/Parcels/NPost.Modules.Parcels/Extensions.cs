using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Parcels.Controllers;
using NPost.Modules.Parcels.Core;
using NPost.Shared;

[assembly: InternalsVisibleTo("NPost")]
namespace NPost.Modules.Parcels
{
    internal static class Extensions
    {
        public static Module AddParcelsModule(this IServiceCollection services)
        {
            services.AddCore();
            
            return new Module(typeof(ParcelsApiController).Assembly, "NPost.Modules.Parcels");
        }

        public static IApplicationBuilder UseParcelsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}