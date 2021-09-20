using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class PreQuotationService : IPreQuotationService
    {
        private readonly IRepository<PreQuotation> _preQuotationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PreQuotationService(
            IRepository<PreQuotation> preQuotationRepository,
            IUnitOfWork unitOfWork)
        {
            _preQuotationRepository = preQuotationRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the pre quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">quotation</exception>
        public async Task<bool> AddPreQuotationAsync(PreQuotation quotation)
        {
            if (quotation == null)
                throw new ArgumentNullException(nameof(quotation));
            try
            {
                await _preQuotationRepository.AddAsync(quotation);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the pre quotation asynchronous.
        /// </summary>
        /// <param name="quotation">The quotation.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">quotation</exception>
        public async Task<bool> DeletePreQuotationAsync(PreQuotation quotation)
        {
            if (quotation == null)
                throw new ArgumentNullException(nameof(quotation));
            try
            {
                _preQuotationRepository.Remove(quotation);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the pre quotation by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<PreQuotation> GetPreQuotationByKeyAsync(string key)
        {
            return await _preQuotationRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }

        /// <summary>
        /// Gets all pre quotations by purchase order asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PreQuotation>> GetAllPreQuotationsByPurchaseOrderAsync(Requisition requisition)
        {
            return await _preQuotationRepository.GetAllAsync(
                predicate: x => x.Requisition.Equals(requisition));
        }
    }
}
