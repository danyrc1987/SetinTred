using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class CustomerQuoteService : ICustomerQuoteService
    {
        private readonly IRepository<CustomerQuote> _customerQuoteService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerQuoteService(
            IRepository<CustomerQuote> customerQuoteService,
            IUnitOfWork unitOfWork)
        {
            _customerQuoteService = customerQuoteService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCustomerQuoteAsync(CustomerQuote customerQuote)
        {
            if (customerQuote == null)
                throw new ArgumentNullException(nameof(customerQuote));
            try
            {
                await _customerQuoteService.AddAsync(customerQuote);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomerQuoteAsync(CustomerQuote customerQuote)
        {
            if (customerQuote == null)
                throw new ArgumentNullException(nameof(customerQuote));
            try
            {
                _customerQuoteService.Update(customerQuote);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CustomerQuote> GetCustomerQuoteByKeyAsync(string key)
        {
            return await _customerQuoteService.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.Currency)
                     .Include(x => x.User)
                     .Include(x => x.Customer)
                     .Include(x => x.CustomerQuoteDetails));
        }

        public async Task<IEnumerable<CustomerQuote>> GetAllCustomerQuotesAsync()
        {
            return await _customerQuoteService.GetAllAsync(
                include: i => i.Include(x => x.Currency)
                    .Include(x => x.User)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerQuoteDetails));
        }

        public async Task<IEnumerable<CustomerQuote>> GetAllCustomerQuotesByUserAsync(UserCore userCore)
        {
            return await _customerQuoteService.GetAllAsync(
                predicate: x => x.User.Equals(userCore),
                include: i => i.Include(x => x.Currency)
                    .Include(x => x.User)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerQuoteDetails));
        }

        public async Task<int> GetCountAllAsync()
        {
            return await _customerQuoteService.CountAsync();
        }

        public async Task<int> GetCountAllByNormativeAsync(string normative)
        {
            return await _customerQuoteService.CountAsync(
                predicate: x => x.NormativeData.Equals(normative));
        }
    }
}
