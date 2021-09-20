using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IPreQuotationService
    {

        /// <summary>
        /// Adds the pre quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        Task<bool> AddPreQuotationAsync(PreQuotation quotation);

        /// <summary>
        /// Deletes the pre quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        Task<bool> DeletePreQuotationAsync(PreQuotation quotation);

        /// <summary>
        /// Gets the pre quotation by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<PreQuotation> GetPreQuotationByKeyAsync(string key);

        /// <summary>
        /// Gets all pre quotations by purchase order asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        Task<IEnumerable<PreQuotation>> GetAllPreQuotationsByPurchaseOrderAsync(Requisition requisition);
    }
}
