namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data
{
    using Core.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EntityTypeConfiguration<T> : IMappingConfiguration, IEntityTypeConfiguration<T> where T : BaseEntity
    {
        protected virtual void PostConfigure(EntityTypeBuilder<T> builder)
        {
        }


        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            PostConfigure(builder);
        }
    }
}
