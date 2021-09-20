namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RequisitionDetailEntityConfiguration : EntityTypeConfiguration<RequisitionDetail>
    {
        public override void Configure(EntityTypeBuilder<RequisitionDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Consecutive).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.MeasurementUnit).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Specification).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Review).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.ReasonForRejection).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.QuantityDispatched).IsRequired(false);
            builder.Property(x => x.QuantityToBuy).IsRequired(false);

            builder.HasOne(requisitionDetail => requisitionDetail.Requisition)
                .WithMany(requisitions => requisitions.RequisitionDetails)
                .HasForeignKey(requisitionDetail => requisitionDetail.RequisitionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
