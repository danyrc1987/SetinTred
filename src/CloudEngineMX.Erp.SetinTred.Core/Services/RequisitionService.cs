using System.Linq;
using CloudEngineMX.Erp.SetinTred.Core.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IRequisitionService" />
    public class RequisitionService : IRequisitionService
    {
        private readonly IRepository<Requisition> _requisitionRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequisitionService"/> class.
        /// </summary>
        /// <param name="requisitionRepository">The requisition repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public RequisitionService(
            IRepository<Requisition> requisitionRepository,
            IUnitOfWork unitOfWork)
        {
            _requisitionRepository = requisitionRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">requisition</exception>
        public async Task<bool> AddRequisitionAsync(Requisition requisition)
        {
            if (requisition == null)
                throw new ArgumentNullException(nameof(requisition));
            try
            {
                await _requisitionRepository.AddAsync(requisition);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the requisition asynchronous.
        /// </summary>
        /// <param name="requisition">The requisition.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">requisition</exception>
        public async Task<bool> UpdateRequisitionAsync(Requisition requisition)
        {
            if (requisition == null)
                throw new ArgumentNullException(nameof(requisition));
            try
            {
                _requisitionRepository.Update(requisition);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets all requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Requisition>> GetAllRequisitionsAsync()
        {
            return await _requisitionRepository.GetAllAsync(
                include: i => i.Include(x => x.Area)
                    .Include(x => x.RequisitionDetails)
                    .Include(x => x.OperatingBase)
                    .Include(x => x.UserCore).ThenInclude(x => x.Report));
        }
        /// <summary>
        /// Gets all requisitions by user asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Requisition>> GetAllRequisitionsByUserAsync(UserCore userCore)
        {
            return await _requisitionRepository.GetAllAsync(
                predicate: x => x.UserCore.Equals(userCore),
                include: i => i.Include(x => x.Area)
                    .Include(x => x.OperatingBase)
                    .Include(x => x.UserCore).ThenInclude(x => x.Report));
        }
        /// <summary>
        /// Gets the requisition by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Requisition> GetRequisitionByKeyAsync(string key)
        {
            return await _requisitionRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.Area)
                    .Include(x => x.OperatingBase)
                    .Include(x => x.RequisitionDetails)
                    .Include(x => x.PreQuotations)
                    .Include(x => x.UserCore).ThenInclude(x => x.Report));
        }

        public async Task<Requisition> GetRequisitionByCodeAsync(string code)
        {
            return await _requisitionRepository.FirstOrDefaultAsync(
                predicate: x => x.RequisitionCode.Equals(code),
                include: i => i.Include(x => x.Area)
                    .Include(x => x.OperatingBase)
                    .Include(x => x.RequisitionDetails)
                    .Include(x => x.UserCore).ThenInclude(x => x.Report));
        }

        /// <summary>
        /// Gets all classification combo.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAllClassificationCombo()
        {
            var classifications = new List<ClassificationType>
            {
                new ClassificationType {Text = "SGC", Value = "SGC"},
                new ClassificationType {Text = "General", Value = "General"},
                new ClassificationType {Text = "Varios", Value = "Varios"}
            };

            var lst = classifications.Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text,
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Seleccionar una Clasificación]"
            });

            return lst;
        }
        /// <summary>
        /// Gets the count all requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAllRequisitionsAsync()
        {
            return await _requisitionRepository.CountAsync();
        }

        /// <summary>
        /// Gets the combo requisitions asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetComboRequisitionsAsync()
        {
            var requisitions = await _requisitionRepository.GetAllAsync(
                predicate: x => x.Status.Equals("Compra Autorizada"),
                include: i => i.Include(x => x.UserCore));

            var lst = requisitions.Select(x => new SelectListItem
            {
                Value = x.RequisitionCode,
                Text = x.RequisitionCode + " - " + x.UserCore.FullName + " [ " + x.Application + " ] "
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona una requisicion]"
            });

            return lst;
        }

        public async Task<IEnumerable<Requisition>> GetAllRequisitionToPurchaseAsync()
        {
            var requisitions = await _requisitionRepository.GetAllAsync(
                include: o => o.Include(x => x.UserCore)
                     .Include(x => x.Area)
                     .Include(x => x.OperatingBase),
                predicate: x => x.Status.Equals("Compra Autorizada") && x.RequisitionDetails.Sum(i => i.QuantityToBuy) > 0);

            return requisitions;
        }
    }
}
