using AutoMapper;
using JobSearch.DTO.Core;
using JobSearch.Models.Core;

namespace JobSearch.Mappings
{
    public class PagedMapper : Profile
    {
        public PagedMapper()
        {
            CreateMap<PagedRequestModel, PagedRequestDTO>();
        }
    }
}
