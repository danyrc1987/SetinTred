namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentEntityConfiguration : EntityTypeConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.PaymentDate).IsRequired().HasColumnType("date");
            builder.Property(x => x.Key).IsRequired(true);
            builder.Property(x => x.Reference).IsRequired(false);
            builder.Property(x => x.Remarks).IsRequired(false);
            builder.Property(x => x.PaymentType).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Ammount).IsRequired(true).HasColumnType("decimal(18,2)");

            builder.HasOne(payment => payment.PurchaseOrder)
                .WithMany(purchase => purchase.Payments)
                .HasForeignKey(payment => payment.PurcharseOrderId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(payment => payment.Currency)
                .WithMany(currency => currency.Payments)
                .HasForeignKey(payment => payment.CurrencyId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(payment => payment.Supplier)
                .WithMany(supplier => supplier.Payments)
                .HasForeignKey(payment => payment.SupplierId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        }
    }
}
