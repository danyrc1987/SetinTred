using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IRequisitionService
    {
        /// <summary>
        /// Adds the requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<bool> AddRequisitionAsync(Requisition requisition);
        /// <summary>
        /// Updates the requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<bool> UpdateRequisitionAsync(Requisition requisition);
        /// <summary>
        /// Gets all requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Requisition>> GetAllRequisitionsAsync();
        /// <summary>
        /// Gets all requisitions by user asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        Task<IEnumerable<Requisition>> GetAllRequisitionsByUserAsync(UserCore userCore);
        /// <summary>
        /// Gets the requisition by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Requisition> GetRequisitionByKeyAsync(string key);
        /// <summary>
        /// Gets the requisition by code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        Task<Requisition> GetRequisitionByCodeAsync(string code);
        /// <summary>
        /// Gets all classification combo.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllClassificationCombo();
        /// <summary>
        /// Gets the count all requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAllRequisitionsAsync();

        /// <summary>
        /// Gets the combo requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetComboRequisitionsAsync();

        /// <summary>
        /// Gets all requisition to purchase asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Requisition>> GetAllRequisitionToPurchaseAsync();
    }
}
