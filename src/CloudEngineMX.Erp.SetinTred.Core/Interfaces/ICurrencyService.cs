namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Gets the currency combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetCurrencyComboAsync();

        /// <summary>
        /// Gets the currency by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<Currency> GetCurrencyByKeyAsync(string key);
    }
}
