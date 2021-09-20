using Microsoft.EntityFrameworkCore;

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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IMeasurementMaterialService" />
    public class MeasurementMaterialService : IMeasurementMaterialService
    {
        private readonly IRepository<MeasurementMaterial> _measurementMaterialRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementMaterialService"/> class.
        /// </summary>
        /// <param name="measurementMaterialRepository">The measurement material repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public MeasurementMaterialService(
            IRepository<MeasurementMaterial> measurementMaterialRepository,
            IUnitOfWork unitOfWork)
        {
            _measurementMaterialRepository = measurementMaterialRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all materials asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MeasurementMaterial>> GetAllMaterialsAsync()
        {
            return await _measurementMaterialRepository.GetAllAsync(
                predicate: x => x.IsActive,
                include: i => i.Include(x => x.Manufacturer)
                    .Include(x => x.MaterialInventories));
        }

        /// <summary>
        /// Gets all materials by manufacturer asynchronous.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MeasurementMaterial>> GetAllMaterialsByManufacturerAsync(Manufacturer manufacturer)
        {
            return await _measurementMaterialRepository.GetAllAsync(
                predicate: x => x.IsActive && x.Manufacturer.Equals(manufacturer));
        }

        /// <summary>
        /// Gets all materials combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllMaterialsComboAsync()
        {
            var materials = await _measurementMaterialRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = materials.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.MaterialName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Material]"
            });

            return lst;
        }

        /// <summary>
        /// Gets all materials combo by manufacturer asynchronous.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllMaterialsComboByManufacturerAsync(Manufacturer manufacturer)
        {
            var materials = await _measurementMaterialRepository.GetAllAsync(
                predicate: x => x.IsActive && x.Manufacturer.Equals(manufacturer));

            var lst = materials.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.MaterialName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Material]"
            });

            return lst;
        }

        /// <summary>
        /// Gets the material by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<MeasurementMaterial> GetMaterialByKeyAsync(string key)
        {
            return await _measurementMaterialRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
    }
}
