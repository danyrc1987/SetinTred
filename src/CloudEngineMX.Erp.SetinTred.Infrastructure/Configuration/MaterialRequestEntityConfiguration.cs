namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Configuration
{
    using Core.Entities;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MaterialRequestEntityConfiguration : EntityTypeConfiguration<MaterialRequest>
    {
        public override void Configure(EntityTypeBuilder<MaterialRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RequestCode).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DispatchedDate).IsRequired(false);
            builder.Property(x => x.EntryDate).IsRequired(false);

            builder.HasOne(request => request.UserCore)
                .WithMany(users => users.MaterialRequests)
                .HasForeignKey(request => request.UserId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
