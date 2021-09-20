namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface ISpecificationService
    {
        /// <summary>
        /// Adds the specification asynchronous.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        Task<bool> AddSpecificationAsync(Specification specification);
        /// <summary>
        /// Updates the specification asynchronous.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        Task<bool> UpdateSpecificationAsync(Specification specification);
        /// <summary>
        /// Gets the specification by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Specification> GetSpecificationByKeyAsync(string key);
        /// <summary>
        /// Gets the specification by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Specification> GetSpecificationById(int id);
        /// <summary>
        /// Gets all specifications asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Specification>> GetAllSpecificationsAsync();
        /// <summary>
        /// Gets all specification by specification type asynchronous.
        /// </summary>
        /// <param name="specificationType">Type of the specification.</param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllSpecificationBySpecificationTypeAsync(
            SpecificationType specificationType);
        /// <summary>
        /// Gets all specification combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllSpecificationComboAsync();
    }
}
