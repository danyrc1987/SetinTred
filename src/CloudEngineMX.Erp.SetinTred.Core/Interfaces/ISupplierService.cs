using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface ISupplierService
    {
        /// <summary>
        /// Creates the supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        Task<bool> CreateSupplierAsync(Supplier supplier);

        /// <summary>
        /// Updates the supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        Task<bool> UpdateSupplierAsync(Supplier supplier);

        /// <summary>
        /// Gets all supplier asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Supplier>> GetAllSupplierAsync();

        /// <summary>
        /// Gets the supplier by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Supplier> GetSupplierByKeyAsync(string key);

        /// <summary>
        /// Gets the supplier by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Supplier> GetSupplierByIdAsync(int id);

        /// <summary>
        /// Gets the combo suppliers asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetComboSuppliersAsync();

        /// <summary>
        /// Totals the supplier active asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> TotalSupplierActiveAsync();
    }
}
