namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ManufacturerEntityConfiguration : EntityTypeConfiguration<Manufacturer>
    {
        public override void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ManufacturerName).IsRequired().HasMaxLength(150);
        }
    }
}
