namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SpecificationEntityConfiguration : EntityTypeConfiguration<Specification>
    {
        public override void Configure(EntityTypeBuilder<Specification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("Date").IsRequired();
            builder.Property(x => x.SpecificationName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Revision).IsRequired();

            builder.HasOne(spec => spec.SpecificationType)
                .WithMany(types => types.Specifications)
                .HasForeignKey(spec => spec.IdSpecificationType);
        }
    }
}
