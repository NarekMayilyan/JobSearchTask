using JobSearch.BLL.Interfaces;
using JobSearch.DAL;
using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace JobSearch.BLL.Implementations
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unitOfWork;
        private IMemoryCache memoryCache;

        public JobService(IUnitOfWork unitOfWork,
            IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }

        public async Task<PagedResponseDTO<JobDTO>> Search(SearchJobDTO dto) => await unitOfWork.JobRepository.Search(dto);

        public async Task<JobDetailedDTO> Get(int id)
        {
            JobDetailedDTO job = null;
            //if(!memoryCache.TryGetValue(id, out job))
            //{
            //    job = await unitOfWork.JobRepository.Get(id);
            //    if (job != null)
            //    {
            //        memoryCache.Set(id, job, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
            //    }
            //}
            job = await unitOfWork.JobRepository.Get(id);
            return job;
        }

        public async Task BookmarkJob(int id, int userId, bool state)
        {
            await unitOfWork.JobRepository.BookmarkJob(id, userId, state);
            await unitOfWork.SaveAsync();
        }
    }
}
