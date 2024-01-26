using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class MoneyScopeConfig : IEntityTypeConfiguration<MoneyScope>
    {
        public void Configure(EntityTypeBuilder<MoneyScope> builder)
        {
            builder.ToTable("MoneyScope");

            builder.HasKey(e => e.id);
            builder.Property(e => e.description)
                .HasMaxLength(50)
                .HasColumnName("description");
            builder.Property(e => e.max_value).HasColumnName("max_value");
            builder.Property(e => e.min_value).HasColumnName("min_value");
        }
    }
}
