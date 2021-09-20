namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SupplierEntityConfiguration : EntityTypeConfiguration<Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

            builder.Property(supplier => supplier.Id).ValueGeneratedOnAdd();
            builder.Property(supplier => supplier.FiscalName).HasMaxLength(250).IsRequired();
            builder.Property(supplier => supplier.Services).HasMaxLength(500).IsRequired();
            builder.Property(supplier => supplier.Specification).HasMaxLength(150).IsRequired();
            builder.Property(supplier => supplier.SpecificationAvailability).HasMaxLength(150).IsRequired(false);
            builder.Property(supplier => supplier.NextScheduledEvaluation).HasColumnType("Date").IsRequired();
            builder.Property(supplier => supplier.Address).IsRequired(false);
            builder.Property(supplier => supplier.Rfc).IsRequired(false);
            builder.Property(supplier => supplier.Phone).IsRequired(false);

            builder.HasOne(supplier => supplier.OperatingBase)
                .WithMany(operating => operating.Suppliers)
                .HasForeignKey(x => x.IdOperatingBase).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(supplier => supplier.ConfidenceLevel)
                .WithMany(confidence => confidence.Suppliers)
                .HasForeignKey(supplier => supplier.IdConfidenceLevel).IsRequired().OnDelete(DeleteBehavior.Restrict);

        }
    }
}
