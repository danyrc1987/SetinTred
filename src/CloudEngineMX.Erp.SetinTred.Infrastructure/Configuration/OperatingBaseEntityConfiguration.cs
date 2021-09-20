namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OperatingBaseEntityConfiguration : EntityTypeConfiguration<OperatingBase>
    {
        public override void Configure(EntityTypeBuilder<OperatingBase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("Date").IsRequired();
            builder.Property(x => x.OperatingBaseName).HasMaxLength(150).IsRequired();
        }
    }
}
