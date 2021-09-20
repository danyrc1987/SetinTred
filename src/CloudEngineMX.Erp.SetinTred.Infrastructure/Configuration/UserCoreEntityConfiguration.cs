namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserCoreEntityConfiguration : EntityTypeConfiguration<UserCore>
    {
        public override void Configure(EntityTypeBuilder<UserCore> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(50);

            builder.HasOne(user => user.Report)
                .WithMany()
                .HasForeignKey(user => user.ReportId).IsRequired(false);

        }
    }
}
