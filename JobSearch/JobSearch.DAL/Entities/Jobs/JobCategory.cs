using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace JobSearch.DAL.Entities.Jobs
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<JobCategoryRef> JobCategoryRefs { get; set; }
    }

    internal class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
    {
        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.ToTable("JobCategories");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}
