using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NPost.Shared;
using NPost.Shared.Commands;
using NPost.Shared.Events;
using NPost.Shared.Options;
using NPost.Shared.Queries;

namespace NPost.Infrastructure
{
    internal static class Extensions
    {
        internal static IServiceCollection AddModules(this IServiceCollection services, IEnumerable<Module> modules)
        {
            var mvcBuilder = services.AddMvc();
            var embeddedFileProviders = new List<EmbeddedFileProvider>();
            foreach (var module in modules)
            {
                mvcBuilder.AddApplicationPart(module.Assembly);
                embeddedFileProviders.Add(new EmbeddedFileProvider(module.Assembly, module.BaseNamespace));
            }

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new CompositeFileProvider(embeddedFileProviders));
            });

            return services;
        }

        internal static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<AppOptions>(configuration.GetSection("app"));
            services.AddMemoryCache();

            var redisSection = configuration.GetSection("redis");
            var redisOptions = new RedisOptions();
            redisSection.Bind(redisOptions);
            services.Configure<RedisOptions>(redisSection);
            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = redisOptions.ConnectionString;
                r.InstanceName = redisOptions.Instance;
            });

            services.Configure<EfOptions>(configuration.GetSection("ef"));
            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase();

            services.AddTransient<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.AddTransient<IIntegrationEventDispatcher, InMemoryIntegrationEventDispatcher>();
            services.AddTransient<IQueryDispatcher, InMemoryQueryDispatcher>();
            services.AddTransient<IDispatcher, InMemoryDispatcher>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IIntegrationEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddTransient<ErrorHandlerMiddleware>();

            return services;
        }
        
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}