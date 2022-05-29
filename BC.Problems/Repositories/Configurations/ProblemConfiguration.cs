using BC.Problems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Problems.Repositories.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(p => p.Address);

        builder.Property(x => x.Stage)
            .HasDefaultValue(ProblemStage.New);

        builder.Property(x => x.Stage)
            .HasConversion(
                v => v.ToString(),
                v => (ProblemStage)Enum.Parse(typeof(ProblemStage), v));

    }
}
