namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MaterialRequestDetailEntityConfiguration : EntityTypeConfiguration<MaterialRequestDetail>
    {
        public override void Configure(EntityTypeBuilder<MaterialRequestDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(detail => detail.Material)
                .WithMany(materials => materials.MaterialRequestDetails)
                .HasForeignKey(detail => detail.MeasurementMaterialId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(detail => detail.MaterialRequest)
                .WithMany(requests => requests.MaterialRequestDetails)
                .HasForeignKey(detail => detail.MaterialRequestId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
