using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CurrencyEntityConfiguration : EntityTypeConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CurrencyName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CurrencySymbol).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ExchangeRate).IsRequired();
        }
    }
}
