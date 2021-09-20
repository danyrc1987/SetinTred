namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PurchaseOrderDetailEntityConfiguration : EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrderDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Consecutive).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.OriginalQuantity).IsRequired(false);
            builder.Property(x => x.Unit).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PartNumber).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.UnitPrice).IsRequired();


            builder.HasOne(detail => detail.PurchaseOrder)
                .WithMany(orders => orders.PurchaseOrderDetails)
                .HasForeignKey(detail => detail.PurchaseOrderId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
