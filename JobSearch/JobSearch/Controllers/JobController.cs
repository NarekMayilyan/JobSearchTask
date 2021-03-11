using AutoMapper;
using JobSearch.BLL.Interfaces;
using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using JobSearch.Models.Core;
using JobSearch.Models.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSearch.WEB.Controllers
{
    public class JobController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IJobService jobService;

        public JobController(IMapper mapper,
            IJobService jobService)
        {
            this.mapper = mapper;
            this.jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchJobModel model)
        {
            var searchDTO = mapper.Map<SearchJobModel, SearchJobDTO> (model);
            if (ApiContext.CurrentUser.IsAuthenticated)
            {
                searchDTO.UserId = ApiContext.CurrentUser.UserId;
            }
            var jobsDTO = await jobService.Search(searchDTO);
            var jobsModel = mapper.Map<PagedResponseDTO<JobDTO>, PagedResponseModel<JobModel>>(jobsDTO);
            return Ok(jobsModel);
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 120)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var jobsDTO = await jobService.Get(id);
            if (jobsDTO == null)
            {
                return NotFound();
            }
            var jobsModel = mapper.Map<JobDetailedDTO, JobDetailedModel>(jobsDTO);
            return Ok(jobsModel);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> BookmarkJob([FromQuery] int id, [FromQuery] bool state)
        {
            var userId = ApiContext.CurrentUser.UserId;
            await jobService.BookmarkJob(id, userId.Value, state);
            return Ok();
        }
    }
}
