namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PreQuotationEntityConfiguration : EntityTypeConfiguration<PreQuotation>
    {
        public override void Configure(EntityTypeBuilder<PreQuotation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.RouteFile).HasMaxLength(500).IsRequired();
            builder.Property(x => x.FileName).HasMaxLength(500).IsRequired();

            builder.HasOne(preQuotation => preQuotation.Requisition)
                .WithMany(orders => orders.PreQuotations)
                .HasForeignKey(preQuotation => preQuotation.RequisitionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
