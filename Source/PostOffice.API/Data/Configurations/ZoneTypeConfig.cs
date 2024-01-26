using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class ZoneTypeConfig : IEntityTypeConfiguration<ZoneType>
    {
        public void Configure(EntityTypeBuilder<ZoneType> builder)
        {
            builder.ToTable("ZoneTypes");
            builder.HasKey(e => e.id);
            builder.Property(x => x.zone_description).HasMaxLength(600);
        }
    }
}
