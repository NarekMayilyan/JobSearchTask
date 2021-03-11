using JobSearch.DAL.Interfaces;
using JobSearch.DTO.JobCategory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearch.DAL.Implementations
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly ApplicationContext context;

        public JobCategoryRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<JobCategoryDTO>> Get()
        {
            var categories = await context.JobCategories.AsNoTracking().Select(i => new JobCategoryDTO
            {
                Id = i.Id,
                Name = i.Name
            }).ToListAsync();

            return categories;
        }
    }
}
