using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface ICustomerQuoteDetailService
    {
        Task<bool> CreateDetailAsync(CustomerQuoteDetail detail);
        Task<bool> UpdateDetailAsync(CustomerQuoteDetail detail);
        Task<CustomerQuoteDetail> GetDetailByKeyAsync(string key);
        Task<bool> DeleteDetailAsync(CustomerQuoteDetail detail);
        Task<IEnumerable<CustomerQuoteDetail>> GetAllDetailByQuoteAsync(CustomerQuote customerQuote);
        Task<int> GetCountAllByCustomerQuote(CustomerQuote quote);
    }
}
