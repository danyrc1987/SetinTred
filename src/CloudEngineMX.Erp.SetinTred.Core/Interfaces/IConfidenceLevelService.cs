namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface IConfidenceLevelService
    {
        /// <summary>
        /// Adds the confidence level asynchronous.
        /// </summary>
        /// <param name="confidenceLevel">The confidence level.</param>
        /// <returns></returns>
        Task<bool> AddConfidenceLevelAsync(ConfidenceLevel confidenceLevel);
        /// <summary>
        /// Updates the confidence level asynchronous.
        /// </summary>
        /// <param name="confidenceLevel">The confidence level.</param>
        /// <returns></returns>
        Task<bool> UpdateConfidenceLevelAsync(ConfidenceLevel confidenceLevel);
        /// <summary>
        /// Gets the confidence level by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<ConfidenceLevel> GetConfidenceLevelByKeyAsync(string key);
        /// <summary>
        /// Gets the confidence level by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ConfidenceLevel> GetConfidenceLevelByIdAsync(int id);
        /// <summary>
        /// Gets all confidence levels asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ConfidenceLevel>> GetAllConfidenceLevelsAsync();
        /// <summary>
        /// Gets all confidence levels combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllConfidenceLevelsComboAsync();


    }
}
