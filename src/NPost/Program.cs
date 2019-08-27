using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace NPost
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateWebHostBuilder(args).Build().RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.Enrich.FromLogContext()
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
                    loggerConfiguration.WriteTo.Console();
                });
    }
}
