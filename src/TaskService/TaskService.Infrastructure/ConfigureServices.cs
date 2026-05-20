using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskService.Domain.Repositories;
using TaskService.Infrastructure.Persistence.Context;
using TaskService.Infrastructure.Persistence.Repositories;

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

            services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
