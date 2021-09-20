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

    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierService"/> class.
        /// </summary>
        /// <param name="supplierRepository">The supplier repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public SupplierService(
            IRepository<Supplier> supplierRepository,
            IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">supplier</exception>
        public async Task<bool> CreateSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));
            try
            {
                await _supplierRepository.AddAsync(supplier);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the supplier asynchronous.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">supplier</exception>
        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));
            try
            {
                _supplierRepository.Update(supplier);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all supplier asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Supplier>> GetAllSupplierAsync()
        {
            return await _supplierRepository.GetAllAsync(
                include: i =>
                    i.Include(x => x.ConfidenceLevel)
                        .Include(x => x.OperatingBase));
        }

        /// <summary>
        /// Gets the supplier by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Supplier> GetSupplierByKeyAsync(string key)
        {
            return await _supplierRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }

        /// <summary>
        /// Gets the supplier by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }

        /// <summary>
        /// Gets the combo suppliers asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetComboSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync(
                include: i => i.Include(x => x.ConfidenceLevel));

            var lst = suppliers.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.FiscalName + " - " + x.ConfidenceLevel.ConfidenceLevelName + "[ " + x.Services + " ]"
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = String.Empty,
                Text = "[Selecciona un Proveedor]"
            });

            return lst;
        }

        /// <summary>
        /// Totals the supplier active asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> TotalSupplierActiveAsync()
        {
            return await _supplierRepository.CountAsync();
        }


    }
}
