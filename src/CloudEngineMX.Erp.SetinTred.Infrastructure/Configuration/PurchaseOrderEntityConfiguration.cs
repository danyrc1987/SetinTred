namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PurchaseOrderEntityConfiguration : EntityTypeConfiguration<PurchaseOrder>
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.PurchaseOrderCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.RequisitionCode).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.Applicant).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.SendTo).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Condition).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Remarks).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.DeliveryTime).IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.TotalInLetters).IsRequired(false).HasMaxLength(700);
            builder.Property(x => x.CancelComments).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DigitalSignature).IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.KeyDescription).IsRequired(false).HasMaxLength(1500);
            builder.Property(x => x.LiberationDate).IsRequired(false);

            builder.HasOne(order => order.UserCore)
                .WithMany(users => users.PurchaseOrders)
                .HasForeignKey(order => order.UserId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(order => order.OperatingBase)
                .WithMany(bases => bases.PurchaseOrders)
                .HasForeignKey(order => order.OperatingBaseId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(order => order.Supplier)
                .WithMany(suppliers => suppliers.PurchaseOrders)
                .HasForeignKey(order => order.SupplierId);
            builder.HasOne(order => order.Currency)
                .WithMany(currencies => currencies.PurchaseOrders)
                .HasForeignKey(currency => currency.CurrencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
