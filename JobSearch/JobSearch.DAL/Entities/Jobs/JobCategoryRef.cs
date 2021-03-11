using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearch.DAL.Entities.Jobs
{
    public class JobCategoryRef
    {
        public int JobCategoryId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public JobCategory JobCategory { get; set; }
    }

    internal class JobCategoryRefConfiguration : IEntityTypeConfiguration<JobCategoryRef>
    {
        public void Configure(EntityTypeBuilder<JobCategoryRef> builder)
        {
            builder.ToTable("JobCategoryRefs");
            builder.HasKey(p => new { p.JobCategoryId, p.JobId });

            builder.HasOne(p => p.JobCategory)
                .WithMany(p => p.JobCategoryRefs).HasForeignKey(p => p.JobCategoryId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Job)
                .WithMany(p => p.JobCategoryRefs).HasForeignKey(p => p.JobId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
