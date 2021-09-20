using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CustomerQuoteEntityConfiguration : EntityTypeConfiguration<CustomerQuote>
    {
        public override void Configure(EntityTypeBuilder<CustomerQuote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.QuoteCode).IsRequired().HasMaxLength(25);
            builder.Property(x => x.LegalDocumentation).IsRequired().HasMaxLength(500);
            builder.Property(x => x.TechnicalData).IsRequired().HasMaxLength(500);
            builder.Property(x => x.NormativeData).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ManufacturingStandard).IsRequired().HasMaxLength(500);
            builder.Property(x => x.QualityProcess).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Validity).IsRequired().HasMaxLength(50);
            builder.Property(x => x.QuoteType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PaymentType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Warranty).IsRequired().HasMaxLength(500);
            builder.Property(x => x.DeliveryTime).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Lab).IsRequired(false).HasMaxLength(1500);

            builder.HasOne(quote => quote.Customer)
                .WithMany(customers => customers.CustomerQuotes)
                .HasForeignKey(quote => quote.CustomerId);
            builder.HasOne(quote => quote.User)
                .WithMany(users => users.CustomerQuotes)
                .HasForeignKey(quote => quote.UserId);
            builder.HasOne(quote => quote.Currency)
                .WithMany(currencies => currencies.CustomerQuotes)
                .HasForeignKey(quote => quote.CurrencyId);

        }
    }
}
