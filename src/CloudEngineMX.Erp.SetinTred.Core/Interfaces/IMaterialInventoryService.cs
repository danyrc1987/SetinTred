namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IMaterialInventoryService
    {
        /// <summary>
        /// Gets all inventories asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MaterialInventory>> GetAllInventoriesAsync();

        /// <summary>
        /// Gets all inventories by material asynchronous.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns></returns>
        Task<IEnumerable<MaterialInventory>> GetAllInventoriesByMaterialAsync(MeasurementMaterial material);

    }
}
