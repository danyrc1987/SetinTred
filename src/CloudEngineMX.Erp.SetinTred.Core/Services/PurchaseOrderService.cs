using System.Linq;
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
    /// Implementation Purchase Order Service
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IPurchaseOrderService" />
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IRepository<PurchaseOrder> _purchaseOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderService"/> class.
        /// </summary>
        /// <param name="purchaseOrderRepository">The purchase order repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public PurchaseOrderService(
            IRepository<PurchaseOrder> purchaseOrderRepository,
            IUnitOfWork unitOfWork)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the purchase order asynchronous.
        /// </summary>
        /// <param name="purchaseOrder">The purchase order.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">purchaseOrder</exception>
        public async Task<bool> AddPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));
            try
            {
                await _purchaseOrderRepository.AddAsync(purchaseOrder);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the purchase order asynchronous.
        /// </summary>
        /// <param name="purchaseOrder">The purchase order.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">purchaseOrder</exception>
        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));
            try
            {
                _purchaseOrderRepository.Update(purchaseOrder);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all purchase order asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderAsync()
        {
            return await _purchaseOrderRepository.GetAllAsync(
                include: i =>
                    i.Include(x => x.UserCore)
                        .Include(x => x.Currency)
                        .Include(x => x.OperatingBase)
                        .Include(x => x.Supplier)
                        .Include(x => x.PurchaseOrderDetails));
        }

        /// <summary>
        /// Gets all purchase order by status asynchronous.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderByStatusAsync(string status)
        {
            return await _purchaseOrderRepository.GetAllAsync(
                predicate: x => x.Status.Equals(status),
                include: i =>
                    i.Include(x => x.UserCore)
                        .Include(x => x.OperatingBase)
                        .Include(x => x.Supplier)
                        .Include(x => x.Currency)
                        .Include(x => x.PurchaseOrderDetails));
        }

        /// <summary>
        /// Gets the purchase order by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<PurchaseOrder> GetPurchaseOrderByKeyAsync(string key)
        {
            return await _purchaseOrderRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i =>
                    i.Include(x => x.UserCore)
                        .Include(x => x.OperatingBase)
                        .Include(x => x.Supplier)
                        .Include(x => x.PurchaseOrderDetails)
                        .Include(x => x.Currency)
                        .Include(x => x.Quotations));
        }

        /// <summary>
        /// Gets the total orders asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTotalOrdersAsync()
        {
            var total = await _purchaseOrderRepository.CountAsync();

            return total + 1;
        }

        /// <summary>
        /// Gets all purchase order by supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrderBySupplierAsync(Supplier supplier)
        {
            return await _purchaseOrderRepository.GetAllAsync(
                predicate: x => x.Supplier.Equals(supplier),
                include: i => i.Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.OperatingBase)
                    .Include(x => x.PurchaseOrderDetails));
        }

        /// <summary>
        /// Gets the purchase order for payment.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetPurchaseOrderForPayment()
        {
            var orders = await _purchaseOrderRepository.GetAllAsync(
                predicate: x => x.Status.Equals("Aprobada"),
                include: i => i.Include(x => x.PurchaseOrderDetails)
                    .Include(x => x.Currency));

            var lst = orders.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.PurchaseOrderCode + " - " +
                       x.PurchaseOrderDetails.Sum(d => d.Quantity * d.UnitPrice).ToString("C2") + " - " +
                       x.Currency.CurrencyName + " - " + x.KeyDescription,
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona una Instrucción de Compra]"
            });

            return lst;
        }
    }
}
