using JobSearch.Common.Enums;
using JobSearch.DTO.Core;
using JobSearch.DTO.GeoLocation;
using System;
using System.Collections.Generic;

namespace JobSearch.DTO.Job
{
    public class SearchJobDTO : PagedRequestDTO
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public EmploymentType? Type { get; set; }
        public int[] Categories { get; set; }
        public GeoLocationDTO Location { get; set; }
    }
}
