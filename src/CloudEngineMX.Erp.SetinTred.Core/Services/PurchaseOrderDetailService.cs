using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IPurchaseOrderDetailService" />
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private readonly IRepository<PurchaseOrderDetail> _purchaseOrderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderDetailService"/> class.
        /// </summary>
        /// <param name="purchaseOrderDetailRepository">The purchase order detail repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public PurchaseOrderDetailService(
            IRepository<PurchaseOrderDetail> purchaseOrderDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the purchase order detail asynchronous.
        /// </summary>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">detail</exception>
        public async Task<bool> AddPurchaseOrderDetailAsync(PurchaseOrderDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                await _purchaseOrderDetailRepository.AddAsync(detail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the purchase order detail asynchronous.
        /// </summary>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">detail</exception>
        public async Task<bool> UpdatePurchaseOrderDetailAsync(PurchaseOrderDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                _purchaseOrderDetailRepository.Update(detail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all items by order asynchronous.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PurchaseOrderDetail>> GetAllItemsByOrderAsync(PurchaseOrder order)
        {
            return await _purchaseOrderDetailRepository.GetAllAsync(
                predicate: x => x.PurchaseOrder.Equals(order));
        }

        /// <summary>
        /// Gets the detail by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<PurchaseOrderDetail> GetDetailByKeyAsync(string key)
        {
            return await _purchaseOrderDetailRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.PurchaseOrder));
        }

        /// <summary>
        /// Gets the total items count asynchronous.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public async Task<int> GetTotalItemsCountAsync(PurchaseOrder order)
        {
            var total = await _purchaseOrderDetailRepository.CountAsync(
                predicate: x => x.PurchaseOrder.Equals(order));

            return total + 1;
        }

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="detail">The detail.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">detail</exception>
        public async Task<bool> DeleteItemAsync(PurchaseOrderDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                _purchaseOrderDetailRepository.Remove(detail);
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
