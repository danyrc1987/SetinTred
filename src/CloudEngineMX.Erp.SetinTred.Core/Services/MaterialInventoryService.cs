using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IMaterialInventoryService" />
    public class MaterialInventoryService : IMaterialInventoryService
    {
        private readonly IRepository<MaterialInventory> _materialInventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialInventoryService"/> class.
        /// </summary>
        /// <param name="materialInventoryRepository">The material inventory repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public MaterialInventoryService(
            IRepository<MaterialInventory> materialInventoryRepository,
            IUnitOfWork unitOfWork)
        {
            _materialInventoryRepository = materialInventoryRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all inventories asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialInventory>> GetAllInventoriesAsync()
        {
            return await _materialInventoryRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets all inventories by material asynchronous.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialInventory>> GetAllInventoriesByMaterialAsync(MeasurementMaterial material)
        {
            return await _materialInventoryRepository.GetAllAsync(
                predicate: x => x.MeasurementMaterial.Equals(material),
                include: i => i.Include(x => x.MeasurementMaterial)
                    .ThenInclude(x => x.Manufacturer));
        }
    }
}
