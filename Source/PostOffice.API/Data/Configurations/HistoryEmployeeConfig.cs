using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;
using System.Reflection.Emit;

namespace PostOffice.API.Data.Configurations
{
    public class HistoryEmployeeConfig : IEntityTypeConfiguration<HistoryEmployee>
    {
        public void Configure(EntityTypeBuilder<HistoryEmployee> builder)
        {
            builder.ToTable("HistoryEmployees");
            builder.HasKey(he => new { he.employee_id, he.track_id });
            builder.HasOne<AppUser>(e => e.Employee)
            .WithMany(e => e.HistoryEmployees)
            .HasForeignKey(h => h.employee_id);


            builder.HasOne<TrackHistory>(t => t.TrackHistory)
            .WithMany(e => e.HistoryEmployees)
            .HasForeignKey(h => h.track_id);
        }
    }
}
