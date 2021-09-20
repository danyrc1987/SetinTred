using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    /// <summary>
    /// Servcice tu manage areas
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        /// Creates the area asynchronous.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        Task<bool> CreateAreaAsync(Area area);

        /// <summary>
        /// Updates the area asynchronous.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        Task<bool> UpdateAreaAsync(Area area);

        /// <summary>
        /// Gets all areas asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Area>> GetAllAreasAsync();

        /// <summary>
        /// Gets the area by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Area> GetAreaByKeyAsync(string key);

        /// <summary>
        /// Gets the combo areas asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetComboAreasAsync();
    }
}
