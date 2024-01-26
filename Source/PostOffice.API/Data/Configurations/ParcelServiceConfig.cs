using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class ParcelServiceConfig:  IEntityTypeConfiguration<ParcelService>
    {
        public void Configure(EntityTypeBuilder<ParcelService> builder)
        {
            builder.ToTable("ParcelServices");
            builder.HasKey(p => p.service_id);
            builder.Property(p => p.name);
            builder.Property(p => p.description);
            builder.Property(p => p.delivery_time);
            builder.Property(p => p.status);

            builder.HasMany(e => e.ParcelOrders)
                .WithOne(o => o.ParcelService).HasForeignKey(o => o.service_id);

        }
    }
}
