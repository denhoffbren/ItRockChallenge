using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Application.Interface;
using TaskService.Application.Mapping;
using TaskService.Application.UsesCases;
using TaskService.Application.Validator;

namespace TaskService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskUsesCases, TaskUsesCases>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssemblyContaining<CreatedTaskDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatedTaskDtoValidator>();
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            return services;
        }
    }
}
