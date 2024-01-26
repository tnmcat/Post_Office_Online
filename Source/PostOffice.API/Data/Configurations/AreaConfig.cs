using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class AreaConfig : IEntityTypeConfiguration<Area>
    {       
           

        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");
            builder.HasKey(a =>a.id);
            builder.Property(a => a.area_name);
            builder.HasMany(a => a.Pincodes).WithOne(p => p.Area).HasForeignKey(p =>p.area_id);
        }
    }
}
