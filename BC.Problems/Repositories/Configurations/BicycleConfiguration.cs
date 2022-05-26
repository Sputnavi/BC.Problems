using BC.Problems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Problems.Repositories.Configurations
{
    public class BicycleConfiguration : IEntityTypeConfiguration<Bicycle>
    {
        public void Configure(EntityTypeBuilder<Bicycle> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Model)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(m => m.SerialNumber)
                .HasMaxLength(255);
        }
    }
}
