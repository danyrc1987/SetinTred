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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.ICurrencyService" />
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="currencyRepository">The currency repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CurrencyService(
            IRepository<Currency> currencyRepository,
            IUnitOfWork unitOfWork)
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the currency combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetCurrencyComboAsync()
        {
            var currencies = await _currencyRepository.GetAllAsync();

            var lst = currencies.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.CurrencyCode + " - " + x.CurrencyName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Moneda]"
            });

            return lst;
        }

        /// <summary>
        /// Gets the currency by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Currency> GetCurrencyByKeyAsync(string key)
        {
            return await _currencyRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
    }
}
