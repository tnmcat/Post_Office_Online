using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
     public class AppUserConfig : IEntityTypeConfiguration<AppUser>
        {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                builder.ToTable("AppUsers");             
                builder.Property(x => x.FirstName).HasMaxLength(200);
                builder.Property(x => x.LastName).HasMaxLength(200);
                builder.Property(x => x.Create_date);
                builder.HasMany(x => x.ParcelOrders).WithOne(u => u.AppUser).HasForeignKey(x => x.user_id);
                builder.HasMany(x => x.MoneyOrders).WithOne(u => u.AppUser).HasForeignKey(x => x.user_id);
        }
        }    
}
