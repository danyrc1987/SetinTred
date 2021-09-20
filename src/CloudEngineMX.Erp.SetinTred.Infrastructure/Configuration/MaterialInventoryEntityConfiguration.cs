namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MaterialInventoryEntityConfiguration : EntityTypeConfiguration<MaterialInventory>
    {
        public override void Configure(EntityTypeBuilder<MaterialInventory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Serial).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Resolution).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.DateOfCalibration).IsRequired(false).HasColumnType("Date");
            builder.Property(x => x.DueDateCalibration).IsRequired(false).HasColumnType("Date");
            builder.Property(x => x.CalibrationFrequency).IsRequired(false);
            builder.Property(x => x.State).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Location).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.InactiveSince).IsRequired(false).HasColumnType("Date");
            builder.Property(x => x.ActiveFrom).IsRequired(false).HasColumnType("Date");
            builder.Property(x => x.CertificationNumber).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.HeadOfVerification).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Remarks).IsRequired(false);

            builder.HasOne(inventory => inventory.MeasurementMaterial)
                .WithMany(materials => materials.MaterialInventories)
                .HasForeignKey(inventory => inventory.MaterialId).IsRequired().OnDelete(DeleteBehavior.Restrict);


        }
    }
}
