using JobSearch.BLL.Implementations;
using JobSearch.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.WEB.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<IJobService, JobService>();
        }
    }
}
