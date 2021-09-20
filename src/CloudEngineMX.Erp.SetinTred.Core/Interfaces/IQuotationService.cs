namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IQuotationService
    {
        /// <summary>
        /// Adds the quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        Task<bool> AddQuotationAsync(Quotation quotation);
        /// <summary>
        /// Deletes the quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        Task<bool> DeleteQuotationAsync(Quotation quotation);
        /// <summary>
        /// Gets the quotation by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Quotation> GetQuotationByKeyAsync(string key);
        /// <summary>
        /// Gets all quotations by purchase order asynchronous.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        Task<IEnumerable<Quotation>> GetAllQuotationsByPurchaseOrderAsync(PurchaseOrder order);

        /// <summary>
        /// Adds all pre quotations asynchronous.
        /// </summary>
        /// <param name="quotations">The quotations.</param>
        /// <returns></returns>
        Task<bool> AddAllPreQuotationsAsync(List<Quotation> quotations);
    }
}
