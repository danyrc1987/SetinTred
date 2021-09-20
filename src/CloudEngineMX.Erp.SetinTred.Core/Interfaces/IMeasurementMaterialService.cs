namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface IMeasurementMaterialService
    {
        /// <summary>
        /// Gets all materials asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MeasurementMaterial>> GetAllMaterialsAsync();
        /// <summary>
        /// Gets all materials by manufacturer asynchronous.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <returns></returns>
        Task<IEnumerable<MeasurementMaterial>> GetAllMaterialsByManufacturerAsync(Manufacturer manufacturer);
        /// <summary>
        /// Gets all materials combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllMaterialsComboAsync();
        /// <summary>
        /// Gets all materials combo by manufacturer asynchronous.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllMaterialsComboByManufacturerAsync(Manufacturer manufacturer);
        /// <summary>
        /// Gets the material by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<MeasurementMaterial> GetMaterialByKeyAsync(string key);
    }
}
