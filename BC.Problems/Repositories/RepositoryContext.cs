using BC.Problems.Models;
using BC.Problems.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BC.Problems.Repositories;

public class RepositoryContext : DbContext
{
    public DbSet<Problem> Problems { get; set; }
    public DbSet<PartModelProblem> PartModelProblems { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PartModelProblemConfiguration());
        modelBuilder.ApplyConfiguration(new ProblemConfiguration());
    }
}
