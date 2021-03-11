using JobSearch.Common.Enums;
using JobSearch.Common.Helpers;
using JobSearch.DAL.Entities.Jobs;
using JobSearch.DAL.Entities.Users;
using JobSearch.DAL.Interfaces;
using JobSearch.DTO.Core;
using JobSearch.DTO.Job;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearch.DAL.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationContext context;

        public JobRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<PagedResponseDTO<JobDTO>> Search(SearchJobDTO dto)
        {
            IQueryable<Job> jobs = context.Jobs.AsNoTracking();

            var paged = new PagedResponseDTO<JobDTO>();

            if (dto.Type.HasValue)
            {
                jobs = jobs.Where(x => x.Type == dto.Type.Value);
            }
            if (dto.Location != null)
            {
                if(!string.IsNullOrEmpty(dto.Location.Country))
                {
                    jobs = jobs.Where(x => x.Country == dto.Location.Country);
                }
                if (!string.IsNullOrEmpty(dto.Location.City))
                {
                    jobs = jobs.Where(x => x.City == dto.Location.City);
                }
            }
            if (!string.IsNullOrEmpty(dto.Title))
            {
                jobs = jobs.Where(x => x.Title.ToLower().Contains(dto.Title.ToLower()));
            }
            if (dto.Categories != null && dto.Categories.Length > 0)
            {
                jobs = jobs.Where(i => i.JobCategoryRefs.Any(c => dto.Categories.Contains(c.JobCategoryId)));
            }

            if (!string.IsNullOrEmpty(dto.Sort.Member) && dto.Sort.Direction.HasValue)
            {
                jobs = dto.Sort.Direction == Sorting.ASC ? jobs.OrderBy(dto.Sort.Member) : jobs.OrderByDescending(dto.Sort.Member);
            }

            paged.Count = jobs.Count();
            jobs = jobs.Skip((dto.Page.Number - 1) * dto.Page.Count).Take(dto.Page.Count);

            var jobDTOs = await jobs.Select(i => new JobDTO
            {
                Id = i.Id,
                Title = i.Title,
                Type = i.Type.GetAttribute<DisplayAttribute>().Name,
                CreatedDate = i.CreatedDate,
                City = i.City,
                Country = i.Country,
                Latitude = i.Latitude,
                Longitude = i.Longitude,
                IsBookmarked = dto.UserId.HasValue && i.UserJobRefs.Any(i => i.UserId == dto.UserId.Value)
            }).ToListAsync();

            paged.Data = jobDTOs;
            return paged;
        }

        public async Task<JobDetailedDTO> Get(int id)
        {
            var job = await context.Jobs.Where(i => i.Id == id).Select(i => new JobDetailedDTO
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Type = i.Type.GetAttribute<DisplayAttribute>().Name,
                CreatedDate = i.CreatedDate,
                City = i.City,
                Country = i.Country,
                Latitude = i.Latitude,
                Longitude = i.Longitude
            }).FirstOrDefaultAsync();

            return job;
        }

        public async Task BookmarkJob(int id, int userId, bool state)
        {
            var checkJob = await context.Jobs.AnyAsync(i => i.Id == id);
            if (checkJob)
            {
                var userJobRef = await context.UserJobRefs.FirstOrDefaultAsync(i => i.JobId == id && i.UserId == userId);
                if (userJobRef == null && state)
                {
                    context.UserJobRefs.Add(new UserJobRef { JobId = id, UserId = userId });
                }
                else if (userJobRef != null && !state)
                {
                    context.UserJobRefs.Remove(userJobRef);
                }
            }
            else
            {
                throw new EntryPointNotFoundException("Job Not Found");
            }
        }
    }
}
