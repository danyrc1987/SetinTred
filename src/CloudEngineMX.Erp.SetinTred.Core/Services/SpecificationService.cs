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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.ISpecificationService" />
    public class SpecificationService : ISpecificationService
    {
        private readonly IRepository<Specification> _specificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationService"/> class.
        /// </summary>
        /// <param name="specificationRepository">The specification repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public SpecificationService(
            IRepository<Specification> specificationRepository,
            IUnitOfWork unitOfWork)
        {
            _specificationRepository = specificationRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Adds the specification asynchronous.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">specification</exception>
        public async Task<bool> AddSpecificationAsync(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
            try
            {
                await _specificationRepository.AddAsync(specification);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the specification asynchronous.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">specification</exception>
        public async Task<bool> UpdateSpecificationAsync(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
            try
            {
                _specificationRepository.Update(specification);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the specification by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Specification> GetSpecificationByKeyAsync(string key)
        {
            return await _specificationRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
        /// <summary>
        /// Gets the specification by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Specification> GetSpecificationById(int id)
        {
            return await _specificationRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }
        /// <summary>
        /// Gets all specifications asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Specification>> GetAllSpecificationsAsync()
        {
            return await _specificationRepository.GetAllAsync();
        }
        /// <summary>
        /// Gets all specification by specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllSpecificationBySpecificationTypeAsync(SpecificationType specificationType)
        {
            var specifications = await _specificationRepository.GetAllAsync(
                predicate: x => x.IsActive && x.SpecificationType.Equals(specificationType));

            var lst = specifications.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.SpecificationName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Debes seleccionar una Especificación...]"
            });

            return lst;
        }

        /// <summary>
        /// Gets all specification combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllSpecificationComboAsync()
        {
            var specifications = await _specificationRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = specifications.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.SpecificationName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Debes seleccionar una Especificación...]"
            });

            return lst;
        }
    }
}
