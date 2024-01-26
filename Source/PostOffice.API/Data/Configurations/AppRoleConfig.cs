using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{        
        public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
        {
            public void Configure(EntityTypeBuilder<AppRole> builder)
            {
                builder.ToTable("AppRoles");

                builder.Property(x => x.Description).HasMaxLength(200);

            }
        }
    
}
