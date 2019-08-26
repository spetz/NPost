using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NPost.Modules.Support.Controllers;
using NPost.Modules.Support.Hubs;
using NPost.Shared;

[assembly: InternalsVisibleTo("NPost")]
namespace NPost.Modules.Support
{
    internal static class Extensions
    {
        public static Module AddSupportModule(this IServiceCollection services)
        {
            var signalR = services.AddSignalR();
//            signalR.AddRedis();
            
            return new Module(typeof(SupportApiController).Assembly, "NPost.Modules.Support");
        }

        public static IApplicationBuilder UseSupportModule(this IApplicationBuilder app)
        {
            app.Map("/support/_meta",
                builder => builder.Run(context => context.Response.WriteAsync("Support module")));
//            app.UseStaticFiles(new StaticFileOptions
//                {
//                    FileProvider = new EmbeddedFileProvider(Assembly.GetAssembly(typeof(NPostHub)))
//                })
//                .UseSignalR(r => r.MapHub<NPostHub>("/npost"));
            app.UseStaticFiles()
                .UseSignalR(r => r.MapHub<NPostHub>("/npost"));

            return app;
        }
    }
}