using JobSearch.DAL;
using JobSearch.DAL.Implementations;
using JobSearch.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.WEB.Extensions
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
        }
    }
}
