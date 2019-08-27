using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NPost.Infrastructure;
using NPost.Modules.Deliveries;
using NPost.Modules.Parcels;
using NPost.Modules.Routing;
using NPost.Modules.Support;
using NPost.Modules.Users;
using NPost.Shared;

namespace NPost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var modules = new List<Module>
            {
                services.AddDeliveriesModule(),
                services.AddParcelsModule(),
                services.AddRoutingModule(),
                services.AddSupportModule(),
                services.AddUsersModule(),
            };
            services.AddModules(modules);
            services.AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseErrorHandler();
            app.UseStaticFiles();
            app.UseDeliveriesModule();
            app.UseParcelsModule();
            app.UseRoutingModule();
            app.UseSupportModule();
            app.UseUsersModule();
            app.UseMvc();
        }
    }
}
