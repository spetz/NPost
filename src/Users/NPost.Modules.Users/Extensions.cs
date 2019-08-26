using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NPost.Modules.Users.Controllers;
using NPost.Modules.Users.Core.Repositories;
using NPost.Modules.Users.Infrastructure.InMemory.Repositories;
using NPost.Modules.Users.Infrastructure.Services;
using NPost.Modules.Users.Services;
using NPost.Shared;

[assembly: InternalsVisibleTo("NPost")]
namespace NPost.Modules.Users
{
    internal static class Extensions
    {
        public static Module AddUsersModule(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var jwtSection = configuration.GetSection("jwt");
            services.Configure<JwtOptions>(jwtSection);
            var options = new JwtOptions();
            jwtSection.Bind(options);
            services.AddAuthorization();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.ValidAudience,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime
                    };
                });
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
            services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
            
            return new Module(typeof(UsersApiController).Assembly, "NPost.Modules.Users");
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            
            return app;
        }
    }
}