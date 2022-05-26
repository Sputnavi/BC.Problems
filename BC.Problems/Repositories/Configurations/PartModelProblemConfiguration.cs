using BC.Problems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Problems.Repositories.Configurations;

public class PartModelProblemConfiguration : IEntityTypeConfiguration<PartModelProblem>
{
	public void Configure(EntityTypeBuilder<PartModelProblem> builder)
	{
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => new { x.ProblemId, x.PartModelId })
			.IsUnique();

		builder.HasOne(x => x.Problem)
			.WithMany(pm => pm.PartModelProblems)
			.HasForeignKey(pm => pm.ProblemId);

		builder.Property(x => x.Amount)
			.HasDefaultValue(1);

		builder.Property(x => x.PricePerDetail)
			.HasPrecision(15, 2)
			.IsRequired(false);
	}
}
