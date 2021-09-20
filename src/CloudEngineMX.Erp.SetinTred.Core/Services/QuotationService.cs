namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;

    public class QuotationService : IQuotationService
    {
        private readonly IRepository<Quotation> _quotationRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationService"/> class.
        /// </summary>
        /// <param name="quotationRepository">The quotation repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QuotationService(
            IRepository<Quotation> quotationRepository,
            IUnitOfWork unitOfWork)
        {
            _quotationRepository = quotationRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">quotation</exception>
        public async Task<bool> AddQuotationAsync(Quotation quotation)
        {
            if (quotation == null)
                throw new ArgumentNullException(nameof(quotation));
            try
            {
                await _quotationRepository.AddAsync(quotation);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">quotation</exception>
        public async Task<bool> DeleteQuotationAsync(Quotation quotation)
        {
            if (quotation == null)
                throw new ArgumentNullException(nameof(quotation));
            try
            {
                _quotationRepository.Remove(quotation);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the quotation by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Quotation> GetQuotationByKeyAsync(string key)
        {
            return await _quotationRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }

        /// <summary>
        /// Gets all quotations by purchase order asynchronous.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Quotation>> GetAllQuotationsByPurchaseOrderAsync(PurchaseOrder order)
        {
            return await _quotationRepository.GetAllAsync(
                predicate: x => x.PurchaseOrder.Equals(order));
        }

        /// <summary>
        /// Adds all pre quotations asynchronous.
        /// </summary>
        /// <param name="quotations">The quotations.</param>
        /// <returns></returns>
        public async Task<bool> AddAllPreQuotationsAsync(List<Quotation> quotations)
        {
            try
            {
                await _quotationRepository.AddAsync(quotations);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
