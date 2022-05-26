using BC.Problems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Problems.Repositories.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(m => m.AddressLine1)
			.HasMaxLength(1024)
			.IsRequired();

		builder.Property(m => m.AddressLine2)
			.HasMaxLength(1024);
	}
}
