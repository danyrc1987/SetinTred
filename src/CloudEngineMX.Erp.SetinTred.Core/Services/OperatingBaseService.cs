namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IOperatingBaseService" />
    public class OperatingBaseService : IOperatingBaseService
    {
        private readonly IRepository<OperatingBase> _operatingBaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingBaseService"/> class.
        /// </summary>
        /// <param name="operatingBaseRepository">The operating base repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public OperatingBaseService(
            IRepository<OperatingBase> operatingBaseRepository,
            IUnitOfWork unitOfWork)
        {
            _operatingBaseRepository = operatingBaseRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the operating base asynchronous.
        /// </summary>
        /// <param name="operatingBase">The operating base.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">operatingBase</exception>
        public async Task<bool> AddOperatingBaseAsync(OperatingBase operatingBase)
        {
            if (operatingBase == null)
                throw new ArgumentNullException(nameof(operatingBase));
            try
            {
                await _operatingBaseRepository.AddAsync(operatingBase);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the operating base asynchronous.
        /// </summary>
        /// <param name="operatingBase">The operating base.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">operatingBase</exception>
        public async Task<bool> UpdateOperatingBaseAsync(OperatingBase operatingBase)
        {
            if (operatingBase == null)
                throw new ArgumentNullException(nameof(operatingBase));
            try
            {
                _operatingBaseRepository.Update(operatingBase);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all operating bases asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<OperatingBase>> GetAllOperatingBasesAsync()
        {
            return await _operatingBaseRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets the operating base by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<OperatingBase> GetOperatingBaseByKeyAsync(string key)
        {
            return await _operatingBaseRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }

        /// <summary>
        /// Gets the operating base by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<OperatingBase> GetOperatingBaseByIdAsync(int id)
        {
            return await _operatingBaseRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }

        /// <summary>
        /// Gets the operating base combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetOperatingBaseComboAsync()
        {
            var bases = await _operatingBaseRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = bases.Select(x => new SelectListItem
            {
                Text = x.OperatingBaseName,
                Value = x.Key
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Debes seleccionar una Base]",
                Value = string.Empty
            });

            return lst;
        }

        /// <summary>
        /// Gets the combo bases asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetComboBasesAsync()
        {
            var bases = await _operatingBaseRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = bases.Select(x => new SelectListItem
            {
                Text = x.OperatingBaseName,
                Value = x.Key
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Selecciona una Base Operativa]",
                Value = string.Empty
            });

            return lst;
        }
    }
}
