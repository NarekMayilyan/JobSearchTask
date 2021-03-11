using JobSearch.DTO.JobCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSearch.DAL.Interfaces
{
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategoryDTO>> Get();
    }
}
