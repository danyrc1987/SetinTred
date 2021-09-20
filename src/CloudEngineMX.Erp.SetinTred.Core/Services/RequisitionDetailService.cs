using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IRequisitionDetailService" />
    public class RequisitionDetailService : IRequisitionDetailService
    {
        private readonly IRepository<RequisitionDetail> _requisitionDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequisitionDetailService"/> class.
        /// </summary>
        /// <param name="requisitionDetailRepository">The requisition detail repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public RequisitionDetailService(
            IRepository<RequisitionDetail> requisitionDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _requisitionDetailRepository = requisitionDetailRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Adds the requisition detail asynchronous.
        /// </summary>
        /// <param name="requisitionDetail">The requisition detail.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">requisitionDetail</exception>
        public async Task<bool> AddRequisitionDetailAsync(RequisitionDetail requisitionDetail)
        {
            if (requisitionDetail == null)
                throw new ArgumentNullException(nameof(requisitionDetail));
            try
            {
                await _requisitionDetailRepository.AddAsync(requisitionDetail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the requisition detail asynchronous.
        /// </summary>
        /// <param name="requisitionDetail">The requisition detail.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">requisitionDetail</exception>
        public async Task<bool> UpdateRequisitionDetailAsync(RequisitionDetail requisitionDetail)
        {
            if (requisitionDetail == null)
                throw new ArgumentNullException(nameof(requisitionDetail));
            try
            {
                _requisitionDetailRepository.Update(requisitionDetail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets all requisition detail asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        public async Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetailAsync(Requisition requisition)
        {
            return await _requisitionDetailRepository.GetAllAsync(
                predicate: x => x.Requisition.Equals(requisition));
        }
        /// <summary>
        /// Gets the requisition detail by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<RequisitionDetail> GetRequisitionDetailByKeyAsync(string key)
        {
            return await _requisitionDetailRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.Requisition));
        }

        /// <summary>
        /// Gets the count details by requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        public async Task<int> GetCountDetailsByRequisitionAsync(Requisition requisition)
        {
            return await _requisitionDetailRepository.CountAsync(
                predicate: x => x.Requisition.Equals(requisition));
        }

        public async Task<bool> DeleteItemToRequestByKeyAsync(RequisitionDetail requisitionDetail)
        {
            if (requisitionDetail == null)
                throw new ArgumentNullException(nameof(requisitionDetail));
            try
            {
                _requisitionDetailRepository.Remove(requisitionDetail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the items to purchase order combo asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetItemsToPurchaseOrderComboAsync(Requisition requisition)
        {
            var items = await _requisitionDetailRepository.GetAllAsync(
                predicate: x => x.Requisition.Equals(requisition) && x.IsApproved && x.QuantityToBuy > 0);

            var lst = items.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.QuantityToBuy + " - " + x.Description
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un item]"
            });

            return lst;
        }

        /// <summary>
        /// Gets the item by description and requisition.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="requisitionId">The requisition identifier.</param>
        /// <returns></returns>
        public async Task<RequisitionDetail> GetItemByDescriptionAndRequisition(string description, int requisitionId)
        {
            return await _requisitionDetailRepository.FirstOrDefaultAsync(
                predicate: x => x.RequisitionId.Equals(requisitionId) && x.Description.Equals(description));
        }
    }
}
