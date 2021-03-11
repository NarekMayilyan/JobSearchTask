using JobSearch.Common.Enums;
using JobSearch.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace JobSearch.DAL.Entities.Jobs
{
    public class Job
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Description{ get; set; }
        public EmploymentType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<JobCategoryRef> JobCategoryRefs { get; set; }
        public ICollection<UserJobRef> UserJobRefs { get; set; }
    }

    internal class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Title);

            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500);

            builder.Property(p => p.City).HasMaxLength(50);

            builder.Property(p => p.Country).HasMaxLength(50);

            builder.Property(p => p.Latitude).HasColumnType("decimal(18, 6)");
         
            builder.Property(p => p.Longitude).HasColumnType("decimal(18, 6)");
        }
    }
}
