using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class OfficeBranchConfig : IEntityTypeConfiguration<OfficeBranch>
    {
        public void Configure(EntityTypeBuilder<OfficeBranch> builder)
        {
            builder.ToTable("OfficeBranchs");
            builder.HasKey(x => x.id);
            builder.Property(x => x.district_name);
            builder.Property(x => x.branch_name);
            builder.Property(x => x.address);
            builder.HasOne(x => x.Pincode).WithMany(y => y.OfficeBranches).HasForeignKey(x => x.pincode);
        }
    }
}
