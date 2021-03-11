using System.ComponentModel.DataAnnotations;

namespace JobSearch.Common.Enums
{
    public enum EmploymentType
    {
        [Display(Name = "Full Time")]
        FullTime = 1,

        [Display(Name = "Part Time")]
        PartTime = 2,

        [Display(Name = "Contractor")]
        Contractor = 3,

        [Display(Name = "Intern")]
        Intern = 4,

        [Display(Name = "Seasonal/Temporary")]
        Temporary = 5
    }
}
