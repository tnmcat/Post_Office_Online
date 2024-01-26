using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Configurations
{
    public class ParcelOrderConfig : IEntityTypeConfiguration<ParcelOrder>
    {
        public void Configure(EntityTypeBuilder<ParcelOrder> builder)
        {
            builder.ToTable("ParcelOrder");

            builder.HasKey(e => e.id);
            builder.Property(e => e.receive_date)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.description)
                .HasMaxLength(200);
            builder.Property(e => e.note)
                .HasMaxLength(5000);
            builder.Property(e => e.send_date)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.order_status);
            builder.Property(e => e.parcel_height);
            builder.Property(e => e.parcel_height);
            builder.Property(e => e.parcel_type_id);
            builder.Property(e => e.parcel_weight);
            builder.Property(e => e.parcel_width);
            builder.Property(e => e.payment_method)
                .HasMaxLength(10);
            builder.Property(e => e.payer)
                .HasMaxLength(10);
            builder.Property(e => e.receiver_address)
                .HasMaxLength(200);
            builder.Property(e => e.receiver_email)
                .HasMaxLength(50);
            builder.Property(e => e.receiver_name)
                .HasMaxLength(50);
            builder.Property(e => e.receiver_phone)
                .HasMaxLength(50);  
            builder.Property(e => e.sender_address)
                .HasMaxLength(200);
            builder.Property(e => e.sender_email)
                .HasMaxLength(50);
            builder.Property(e => e.sender_name)
                .HasMaxLength(50);
            builder.Property(e => e.sender_phone)
                .HasMaxLength(20)
                ;            
            
            builder.Property(e => e.total_charge);
            builder.Property(e => e.vpp_value);

            builder.HasOne(d => d.ParcelSenderPincode)
       .WithMany(p => p.SenderPincodePO)
       .HasForeignKey(d => d.sender_pincode).OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(d => d.ParcelReceiverPincode)
       .WithMany(p => p.ReceiverPincodePO)
       .HasForeignKey(d => d.receiver_pincode).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(d => d.TrackHistories).WithOne(p => p.ParcelOrder)
              .HasForeignKey(d => d.order_id)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.ParcelType).WithMany(d => d.ParcelOrders).HasForeignKey(d => d.parcel_type_id);
            builder.HasOne(d => d.ParcelService).WithMany(d => d.ParcelOrders).HasForeignKey(d => d.service_id);
            builder.HasOne(d => d.OrderStatus).WithMany(d => d.ParcelOrders).HasForeignKey(d => d.order_status);

        }
    }
}
