using JobSearch.Common.Enums;
using System;

namespace JobSearch.Models.Job
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsBookmarked { get; set; }
    }

    public class JobDetailedModel : JobModel
    {
        public string Description { get; set; }
    }
}
