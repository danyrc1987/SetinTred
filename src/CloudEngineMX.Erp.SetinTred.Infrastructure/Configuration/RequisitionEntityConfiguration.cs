namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RequisitionEntityConfiguration : EntityTypeConfiguration<Requisition>
    {
        public override void Configure(EntityTypeBuilder<Requisition> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RequisitionCode).IsRequired();
            builder.Property(x => x.Application).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Application).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.KeyDescription).IsRequired(false).HasMaxLength(1500);

            builder.HasOne(requisition => requisition.Area)
                .WithMany(areas => areas.Requisitions)
                .HasForeignKey(requisition => requisition.AreaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(requisition => requisition.UserCore)
                .WithMany(users => users.Requisitions)
                .HasForeignKey(requisition => requisition.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(requisition => requisition.OperatingBase)
                .WithMany(operatingBases => operatingBases.Requisitions)
                .HasForeignKey(requisition => requisition.OperatingBaseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
