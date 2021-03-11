using JobSearch.DTO.JobCategory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.BLL.Interfaces
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategoryDTO>> Get();
    }
}
