using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class WeightScopeConfig : IEntityTypeConfiguration<WeightScope>
    {
        public void Configure(EntityTypeBuilder<WeightScope> builder)
        {
            builder.ToTable("WeightScope");

            builder.HasKey(e => e.id);
            builder.Property(e => e.description)
                .HasMaxLength(1000);
            builder.Property(e => e.max_weight);
            builder.Property(e => e.min_weight);
        }
    }
}
