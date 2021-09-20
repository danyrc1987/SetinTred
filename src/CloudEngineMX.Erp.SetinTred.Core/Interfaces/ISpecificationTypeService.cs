namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface ISpecificationTypeService
    {
        /// <summary>
        /// Adds the specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        Task<bool> AddSpecificationTypeAsync(SpecificationType specificationType);
        /// <summary>
        /// Updates the specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        Task<bool> UpdateSpecificationTypeAsync(SpecificationType specificationType);
        /// <summary>
        /// Gets the specification type by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<SpecificationType> GetSpecificationTypeByKeyAsync(string key);
        /// <summary>
        /// Gets the specification type by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<SpecificationType> GetSpecificationTypeByIdAsync(int id);
        /// <summary>
        /// Gets all specification types asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SpecificationType>> GetAllSpecificationTypesAsync();
        /// <summary>
        /// Gets all specification types combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllSpecificationTypesComboAsync();
    }
}
