using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using System.Threading.Tasks;

namespace JobSearch.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task<PagedResponseDTO<JobDTO>> Search(SearchJobDTO dto);
        Task<JobDetailedDTO> Get(int id);
        Task BookmarkJob(int id, int userId, bool state);
    }
}
