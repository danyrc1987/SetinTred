using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface ICustomerQuoteService
    {
        Task<bool> CreateCustomerQuoteAsync(CustomerQuote customerQuote);
        Task<bool> UpdateCustomerQuoteAsync(CustomerQuote customerQuote);
        Task<CustomerQuote> GetCustomerQuoteByKeyAsync(string key);
        Task<IEnumerable<CustomerQuote>> GetAllCustomerQuotesAsync();
        Task<IEnumerable<CustomerQuote>> GetAllCustomerQuotesByUserAsync(UserCore userCore);
        Task<int> GetCountAllAsync();
        Task<int> GetCountAllByNormativeAsync(string normative);
    }
}
