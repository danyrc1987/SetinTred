using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    /// Interface Purchase Order Service
    /// </summary>
    public interface IPurchaseOrderService
    {
        /// <summary>
        /// Adds the purchase order asynchronous.
        /// </summary>
        /// <param name="purchaseOrder">The purchase order.</param>
        /// <returns></returns>
        Task<bool> AddPurchaseOrderAsync(PurchaseOrder purchaseOrder);
        /// <summary>
        /// Updates the purchase order asynchronous.
        /// </summary>
        /// <param name="purchaseOrder">The purchase order.</param>
        /// <returns></returns>
        Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        /// <summary>
        /// Gets all purchase order asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderAsync();
        /// <summary>
        /// Gets all purchase order by status asynchronous.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderByStatusAsync(string status);
        /// <summary>
        /// Gets the purchase order by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<PurchaseOrder> GetPurchaseOrderByKeyAsync(string key);

        /// <summary>
        /// Gets the total orders asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalOrdersAsync();

        /// <summary>
        /// Gets all purchase order by supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderBySupplierAsync(Supplier supplier);

        /// <summary>
        /// Gets the purchase order for payment.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetPurchaseOrderForPayment();
    }
}
