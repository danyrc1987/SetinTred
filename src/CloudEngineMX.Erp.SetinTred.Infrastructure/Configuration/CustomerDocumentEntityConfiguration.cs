using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    public class CustomerDocumentEntityConfiguration : EntityTypeConfiguration<CustomerDocument>
    {
        public override void Configure(EntityTypeBuilder<CustomerDocument> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired().HasColumnType("date");
            builder.Property(x => x.ExpirationDate).IsRequired().HasColumnType("date");
            builder.Property(x => x.DocumentName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.DocumentType).IsRequired().HasMaxLength(500);
            builder.Property(x => x.DocumentRoute).IsRequired().HasMaxLength(1500);

            builder.HasOne(document => document.Customer)
                .WithMany(customers => customers.CustomerDocuments)
                .HasForeignKey(document => document.CustomerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
