using JobSearch.DAL.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearch.DAL.Entities.Users
{
    public class UserJobRef
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public User User { get; set; }
        public Job Job { get; set; }
    }

    internal class UserJobRefConfiguration : IEntityTypeConfiguration<UserJobRef>
    {
        public void Configure(EntityTypeBuilder<UserJobRef> builder)
        {
            builder.ToTable("UserJobRefs");
            builder.HasKey(p => new { p.UserId, p.JobId });

            builder.HasOne(p => p.User)
                .WithMany(p => p.UserJobRefs).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Job)
                .WithMany(p => p.UserJobRefs).HasForeignKey(p => p.JobId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
