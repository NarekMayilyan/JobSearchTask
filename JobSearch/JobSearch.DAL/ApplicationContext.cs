using JobSearch.DAL.Entities.Jobs;
using JobSearch.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.DAL
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobCategoryRef> JobCategoryRefs { get; set; }
        public DbSet<UserJobRef> UserJobRefs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new JobCategoryConfiguration());
            builder.ApplyConfiguration(new JobCategoryRefConfiguration());
            builder.ApplyConfiguration(new UserJobRefConfiguration());
        }
    }
}
