using BC.Bicycles.Repositories.Configurations;
using BC.Problems.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.Problems.Repositories
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Bicycle> Bicycles { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BicycleConfiguration());
        }
    }
}
