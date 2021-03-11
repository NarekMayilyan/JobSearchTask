using AutoMapper;
using JobSearch.DTO.JobCategory;
using JobSearch.Models.JobCategory;

namespace JobSearch.Mappings
{
    public class JobCategoryMapper : Profile
    {
        public JobCategoryMapper()
        {
            CreateMap<JobCategoryModel, JobCategoryDTO>().ReverseMap();
        }
    }
}
