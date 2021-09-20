namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IManufacturerService" />
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerService"/> class.
        /// </summary>
        /// <param name="manufacturerRepository">The manufacturer repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public ManufacturerService(
            IRepository<Manufacturer> manufacturerRepository,
            IUnitOfWork unitOfWork)
        {
            _manufacturerRepository = manufacturerRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all manufacturers asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _manufacturerRepository.GetAllAsync(
                predicate: x => x.IsActive);
        }

        /// <summary>
        /// Gets all manufacturers combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllManufacturersComboAsync()
        {
            var manufacturers = await _manufacturerRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = manufacturers.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.ManufacturerName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Fabricante]"
            });

            return lst;
        }
    }
}
