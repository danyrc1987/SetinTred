namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<bool> Save();
    }
}
