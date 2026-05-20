using AuthService.Application.Interfaces;
using AuthService.Application.Mapping;
using AuthService.Application.UsesCases;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthUsesCases, AuthUsesCases>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
