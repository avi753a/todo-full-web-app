using Microsoft.Extensions.DependencyInjection;
using TodoApp.Data.Entites;
using TodoApp.Models;
using TodoApp.Service.Contracts;
using TodoApp.Services;

namespace TodoApp.Service
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServiceProviders(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService<StatusView>, CacheService<StatusView>>();
            services.AddScoped<ICacheService<PriorityView>, CacheService<PriorityView>>();
            services.AddAutoMapper(typeof(MapperService));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
