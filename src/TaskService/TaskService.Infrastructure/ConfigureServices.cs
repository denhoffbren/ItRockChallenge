using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TaskService.Application.Interface;
using TaskService.Domain.Repositories;
using TaskService.Infrastructure.Persistence.Context;
using TaskService.Infrastructure.Persistence.Repositories;
using TaskService.Infrastructure.Services;

namespace TaskService.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                o.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT_SECRET_KEY"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    NameClaimType = ClaimTypes.NameIdentifier

                };
            });

            services.AddHttpClient<IExternalApiService, ExternalApiService>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["ExternalApi:BaseUrl"]!);
            });

            services.AddAuthorization();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddHttpClient<IExternalApiService, ExternalApiService>();
            return services;
        }
    }
}
