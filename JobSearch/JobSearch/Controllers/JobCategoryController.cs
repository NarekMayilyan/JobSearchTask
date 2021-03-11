using AutoMapper;
using JobSearch.BLL.Interfaces;
using JobSearch.DTO.JobCategory;
using JobSearch.Models.JobCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSearch.WEB.Controllers
{
    public class JobCategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IJobCategoryService jobCategoryService;

        public JobCategoryController(IMapper mapper,
            IJobCategoryService jobCategoryService)
        {
            this.mapper = mapper;
            this.jobCategoryService = jobCategoryService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoryDTOs = await jobCategoryService.Get();
            var categoryModels = mapper.Map<IEnumerable<JobCategoryDTO>, IEnumerable<JobCategoryModel>>(categoryDTOs);
            return Ok(categoryModels);
        }
    }
}
