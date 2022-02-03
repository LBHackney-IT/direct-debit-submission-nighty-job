using DirectDebitSubmissionNightyJob.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DirectDebitSubmissionNightyJob.Infrastructure
{

    public class DirectDebitContext : DbContext
    {

        public DirectDebitContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DirectDebitDbEntity> DirectDebitDbEntities { get; set; }
        public DbSet<DirectDebitMaintenanceDbEntity> DirectDebitMaintenanceDbEntities { get; set; }
        public DbSet<DirectDebitSubmissionDbEntity> DirectDebitSubmissionDbEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder?.ApplyConfiguration(new DirectDebitConfiguration());
            modelBuilder?.ApplyConfiguration(new DirectDebitMaintenanceConfiguration());
            modelBuilder?.ApplyConfiguration(new DirectDebitSubmissionConfiguration());
        }
    }
}
