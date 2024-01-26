using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class TrackHistoryConfig : IEntityTypeConfiguration<TrackHistory>
    {
        public void Configure(EntityTypeBuilder<TrackHistory> builder)
        {
            builder.ToTable("TrackHistory");
            builder.HasKey(e => e.track_id);
            builder.Property(e => e.new_location)
                .HasMaxLength(50);
            builder.Property(e => e.new_status)
                .HasMaxLength(50);
                
                         
        
          

          
        }
    }
}
