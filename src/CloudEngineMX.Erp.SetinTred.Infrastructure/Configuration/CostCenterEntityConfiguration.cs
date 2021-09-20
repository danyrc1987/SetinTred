using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CostCenterEntityConfiguration : EntityTypeConfiguration<CostCenter>
    {
        public override void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.CostCenterName).HasMaxLength(50).IsRequired();
        }
    }
}
