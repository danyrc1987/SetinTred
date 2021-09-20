using System;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data
{
    using System.Threading.Tasks;
    using CloudEngineMX.Erp.SetinTred.Core.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Save()
        {
            try
            {
                var save = await _dataContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }

        }
    }
}
