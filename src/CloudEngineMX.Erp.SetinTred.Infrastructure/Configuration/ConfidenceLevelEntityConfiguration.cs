namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConfidenceLevelEntityConfiguration : EntityTypeConfiguration<ConfidenceLevel>
    {
        public override void Configure(EntityTypeBuilder<ConfidenceLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("Date").IsRequired();
            builder.Property(x => x.ConfidenceLevelName).HasMaxLength(150).IsRequired();
        }
    }
}
