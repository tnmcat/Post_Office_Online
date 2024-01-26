using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class ParcelTypeConfig : IEntityTypeConfiguration<ParcelType>
    {
        public void Configure(EntityTypeBuilder<ParcelType> builder)
        {
            builder.ToTable("ParcelType");

            builder.HasKey(e => e.id);
            builder.Property(e => e.over_dimension_rate);
            builder.Property(e => e.max_height);
            builder.Property(e => e.max_length);
            builder.Property(e => e.max_width);
            builder.Property(e => e.name)
                .HasMaxLength(100)
                ;
            builder.Property(e => e.description)
              .HasMaxLength(500)
              ;

        }
    }
}
