namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AreaEntityConfiguration : EntityTypeConfiguration<Area>
    {
        public override void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.AreaName).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }
    }
}
