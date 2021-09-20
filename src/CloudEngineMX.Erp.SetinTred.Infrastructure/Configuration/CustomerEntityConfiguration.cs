using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CustomerEntityConfiguration : EntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.FiscalName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.SocialName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Rfc).IsRequired().HasMaxLength(13);
            builder.Property(x => x.PersonType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.AdministrativeContactName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AdministrativeContactEmail).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AdministrativeContactPhone).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FinancialContactName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FinancialContactEmail).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FinancialContactPhone).IsRequired().HasMaxLength(50);
        }
    }
}
