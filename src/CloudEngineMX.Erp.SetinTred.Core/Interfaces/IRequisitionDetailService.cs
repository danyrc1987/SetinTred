using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IRequisitionDetailService
    {
        /// <summary>
        /// Adds the requisition detail asynchronous.
        /// </summary>
        /// <param name="requisitionDetail">The requisition detail.</param>
        /// <returns></returns>
        Task<bool> AddRequisitionDetailAsync(RequisitionDetail requisitionDetail);
        /// <summary>
        /// Updates the requisition detail asynchronous.
        /// </summary>
        /// <param name="requisitionDetail">The requisition detail.</param>
        /// <returns></returns>
        Task<bool> UpdateRequisitionDetailAsync(RequisitionDetail requisitionDetail);
        /// <summary>
        /// Gets all requisition detail asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetailAsync(Requisition requisition);
        /// <summary>
        /// Gets the requisition detail by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<RequisitionDetail> GetRequisitionDetailByKeyAsync(string key);
        /// <summary>
        /// Gets the count details by requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<int> GetCountDetailsByRequisitionAsync(Requisition requisition);

        /// <summary>
        /// Deletes the item to request by key asynchronous.
        /// </summary>
        /// <param name="requisitionDetail">The requisition detail.</param>
        /// <returns></returns>
        Task<bool> DeleteItemToRequestByKeyAsync(RequisitionDetail requisitionDetail);

        /// <summary>
        /// Gets the items to purchase order combo asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetItemsToPurchaseOrderComboAsync(Requisition requisition);

        /// <summary>
        /// Gets the item by description and requisition.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="requisitionId">The requisition identifier.</param>
        /// <returns></returns>
        Task<RequisitionDetail> GetItemByDescriptionAndRequisition(string description, int requisitionId);
    }
}
