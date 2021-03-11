using JobSearch.BLL.Interfaces;
using JobSearch.DAL;
using JobSearch.DTO.JobCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSearch.BLL.Implementations
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public JobCategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JobCategoryDTO>> Get() => await unitOfWork.JobCategoryRepository.Get();
    }
}
