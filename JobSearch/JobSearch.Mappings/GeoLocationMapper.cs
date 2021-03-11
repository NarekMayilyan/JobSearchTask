using AutoMapper;
using JobSearch.DTO.GeoLocation;
using JobSearch.Models.GeoLocation;

namespace JobSearch.Mappings
{
    public class GeoLocationMapper : Profile
    {
        public GeoLocationMapper()
        {
            CreateMap<GeoLocationModel, GeoLocationDTO>().ReverseMap();
        }
    }
}
