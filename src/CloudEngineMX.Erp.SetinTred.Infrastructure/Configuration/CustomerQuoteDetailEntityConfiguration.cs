using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CustomerQuoteDetailEntityConfiguration : EntityTypeConfiguration<CustomerQuoteDetail>
    {
        public override void Configure(EntityTypeBuilder<CustomerQuoteDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Consecutive).IsRequired();
            builder.Property(x => x.TypeItem).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Unit).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Availability).IsRequired().HasMaxLength(250);
            builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.Scope).IsRequired(false);
            builder.Property(x => x.Remarks).IsRequired(false);

            builder.HasOne(detail => detail.CustomerQuote)
                .WithMany(quotes => quotes.CustomerQuoteDetails)
                .HasForeignKey(detail => detail.CustomerQuoteId);

        }
    }
}
