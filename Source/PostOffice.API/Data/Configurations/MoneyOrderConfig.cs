using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;
using System.Reflection.Emit;

namespace PostOffice.API.Data.Configurations
{
    public class MoneyOrderConfig : IEntityTypeConfiguration<MoneyOrder>
    {
        public void Configure(EntityTypeBuilder<MoneyOrder> builder)
        {
            builder.ToTable("MoneyOrder");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();

            builder.Property(x => x.sender_name).IsUnicode(true).HasMaxLength(50);
            builder.Property(x => x.sender_name).IsUnicode(true).HasMaxLength(50);
           
            builder.Property(x => x.sender_phone).HasMaxLength(10);
            builder.Property(x => x.sender_address).IsUnicode(true).HasMaxLength(200);
            builder.Property(x => x.receiver_name).IsUnicode(true).HasMaxLength(50);
          
            builder.Property(x => x.receiver_phone).HasMaxLength(10);
            builder.Property(x => x.receiver_address).IsUnicode(true).HasMaxLength(200);

            builder.Property(x => x.transfer_value);

            builder.Property(x => x.transfer_fee);

            builder.Property(x => x.total_charge);

            builder.Property(x => x.note).HasMaxLength(500);

            builder.Property(x => x.send_date);

            builder.Property(x => x.transfer_status);

            builder.Property(x => x.sender_national_identity_number).HasMaxLength(20);

            builder.Property(x => x.receiver_national_identity_number).HasMaxLength(20);


            builder.HasOne(d => d.MoneySenderPincode)
   .WithMany(p => p.SenderPincodeMO)
   .HasForeignKey(d => d.sender_pincode).OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(d => d.MoneyReceiverPincode)
       .WithMany(p => p.ReceiverPincodeMO)
       .HasForeignKey(d => d.receiver_pincode).OnDelete(DeleteBehavior.NoAction);


        }
       
    }
}
