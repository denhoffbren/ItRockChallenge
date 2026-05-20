using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.Interface;
using TaskService.Application.Mapping;
using TaskService.Application.UsesCases;

namespace TaskService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskUsesCases, TaskUsesCases>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
