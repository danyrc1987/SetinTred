namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
