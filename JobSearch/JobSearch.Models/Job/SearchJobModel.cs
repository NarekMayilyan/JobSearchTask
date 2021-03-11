using JobSearch.Common.Enums;
using JobSearch.Models.Core;
using JobSearch.Models.GeoLocation;

namespace JobSearch.Models.Job
{
    public class SearchJobModel : PagedRequestModel
    {
        public string Title { get; set; }
        public EmploymentType? Type { get; set; }
        public int[] Categories { get; set; }
        public GeoLocationModel Location { get; set; }
    }
}
