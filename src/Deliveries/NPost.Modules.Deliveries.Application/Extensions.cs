using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("NPost")]
[assembly: InternalsVisibleTo("NPost.Modules.Deliveries")]
[assembly: InternalsVisibleTo("NPost.Modules.Deliveries.Infrastructure")]
namespace NPost.Modules.Deliveries.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}