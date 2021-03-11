using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using System.Threading.Tasks;

namespace JobSearch.BLL.Interfaces
{
    public interface IJobService
    {
        Task<PagedResponseDTO<JobDTO>> Search(SearchJobDTO dto);
        Task<JobDetailedDTO> Get(int id);
        Task BookmarkJob(int id, int userId, bool state);
    }
}
