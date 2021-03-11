using AutoMapper;
using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using JobSearch.Models.Core;
using JobSearch.Models.Job;

namespace JobSearch.Mappings
{
    public class JobMapper : Profile
    {
        public JobMapper()
        {
            CreateMap<SearchJobModel, SearchJobDTO>().ReverseMap();
            CreateMap<PagedResponseDTO<JobDTO>, PagedResponseModel<JobModel>>().ReverseMap();
            CreateMap<JobDTO, JobModel>().ReverseMap();
            CreateMap<JobDetailedDTO, JobDetailedModel>().ReverseMap();
        }
    }
}
