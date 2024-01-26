using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class ParcelServicePriceConfig : IEntityTypeConfiguration<ParcelServicePrice>
    {
        
            
               
public void Configure(EntityTypeBuilder<ParcelServicePrice> builder)
        {
            builder.ToTable("ParcelServicePrice");
            builder.HasKey(e => e.parcel_price_id);
            builder.HasOne(e => e.ZoneTypes).WithMany(z => z.ParcelServicePrice).HasForeignKey(z => z.zone_type_id);
            builder.HasOne(e => e.ParcelTypes).WithMany(p => p.ParcelServicePrice).HasForeignKey(p => p.parcel_type_id);
            builder.HasOne(e => e.WeightScopes).WithMany(z => z.ParcelServicePrice).HasForeignKey(w => w.scope_weight_id);
            builder.HasOne(e => e.ParcelServices).WithMany(z => z.ParcelServicePrice).HasForeignKey(w => w.service_id);
            builder.Property(x => x.service_price);
        }
    }
}

