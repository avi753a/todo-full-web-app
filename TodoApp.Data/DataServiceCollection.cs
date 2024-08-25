using Microsoft.Extensions.DependencyInjection;

using TodoApp.Data.Contracts;

namespace TodoApp.Data
{
    public static class DataServiceCollection
    {
        public static IServiceCollection AddDataProviders(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepo, TaskRepo>();
            services.AddScoped<IStatusRepo, StatusRepo>();
            services.AddScoped<IPriorityRepo, PriorityRepo>();
            return services;
        }
    }
}
