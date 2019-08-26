using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Parcels.Core.InMemory.Repositories;
using NPost.Modules.Parcels.Core.Repositories;

[assembly: InternalsVisibleTo("NPost")]
[assembly: InternalsVisibleTo("NPost.Modules.Parcels")]
namespace NPost.Modules.Parcels.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IParcelsRepository, InMemoryParcelsRepository>();

            return services;
        }
    }
}