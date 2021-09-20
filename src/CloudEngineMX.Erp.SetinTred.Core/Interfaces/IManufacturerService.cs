namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface IManufacturerService
    {
        /// <summary>
        /// Gets all manufacturers asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
        /// <summary>
        /// Gets all manufacturers combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllManufacturersComboAsync();
    }
}
