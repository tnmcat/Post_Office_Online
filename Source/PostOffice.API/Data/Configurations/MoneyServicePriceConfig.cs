using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class MoneyServicePriceConfig : IEntityTypeConfiguration<MoneyServicePrice>
    {
        public void Configure(EntityTypeBuilder<MoneyServicePrice> builder)
        {
            builder.ToTable("MoneyServicePrice");

            builder.HasKey(x => x.id);             

            builder.Property(x => x.fee);

            // Xác định mối quan hệ với MoneyScope (một MoneyService thuộc về một MoneyScope)
            builder.HasOne(x => x.MoneyScopes)
                   .WithMany(m =>m.MoneyServicePrice)
                   .HasForeignKey(m =>m.money_scope_id);

            builder.HasOne(e => e.ZoneTypes)
                   .WithMany(m => m.MoneyServicePrice)
                   .HasForeignKey(w => w.zone_type_id);
        }
    }
}
