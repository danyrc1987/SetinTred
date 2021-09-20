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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.ISpecificationTypeService" />
    public class SpecificationTypeService : ISpecificationTypeService
    {
        private readonly IRepository<SpecificationType> _specificationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationTypeService"/> class.
        /// </summary>
        /// <param name="specificationTypeRepository">The specification type repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public SpecificationTypeService(
            IRepository<SpecificationType> specificationTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _specificationTypeRepository = specificationTypeRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Adds the specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">specificationType</exception>
        public async Task<bool> AddSpecificationTypeAsync(SpecificationType specificationType)
        {
            if (specificationType == null)
                throw new ArgumentNullException(nameof(specificationType));
            try
            {
                await _specificationTypeRepository.AddAsync(specificationType);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">specificationType</exception>
        public async Task<bool> UpdateSpecificationTypeAsync(SpecificationType specificationType)
        {
            if (specificationType == null)
                throw new ArgumentNullException(nameof(specificationType));
            try
            {
                _specificationTypeRepository.Update(specificationType);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the specification type by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<SpecificationType> GetSpecificationTypeByKeyAsync(string key)
        {
            return await _specificationTypeRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
        /// <summary>
        /// Gets the specification type by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<SpecificationType> GetSpecificationTypeByIdAsync(int id)
        {
            return await _specificationTypeRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }
        /// <summary>
        /// Gets all specification types asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SpecificationType>> GetAllSpecificationTypesAsync()
        {
            return await _specificationTypeRepository.GetAllAsync();
        }
        /// <summary>
        /// Gets all specification types combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllSpecificationTypesComboAsync()
        {
            var types = await _specificationTypeRepository.GetAllAsync(
                predicate: x => x.IsActive);
            var lst = types.Select(x => new SelectListItem
            {
                Text = x.SpecificationTypeName,
                Value = x.Key
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Debes seleccionar el tipo de especifiación...]",
                Value = string.Empty
            });

            return lst;
        }
    }
}
