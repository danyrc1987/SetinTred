namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MeasurementMaterialEntityConfiguration : EntityTypeConfiguration<MeasurementMaterial>
    {
        public override void Configure(EntityTypeBuilder<MeasurementMaterial> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.MaterialName).IsRequired().HasMaxLength(150);

            builder.HasOne(material => material.Manufacturer)
                .WithMany(manufacturers => manufacturers.MeasurementMaterials)
                .HasForeignKey(material => material.ManufacturerId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
