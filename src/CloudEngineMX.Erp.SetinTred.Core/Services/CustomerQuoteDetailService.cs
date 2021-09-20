using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class CustomerQuoteDetailService : ICustomerQuoteDetailService
    {
        private readonly IRepository<CustomerQuoteDetail> _customerQuoteDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerQuoteDetailService(
            IRepository<CustomerQuoteDetail> customerQuoteDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _customerQuoteDetailRepository = customerQuoteDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateDetailAsync(CustomerQuoteDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                await _customerQuoteDetailRepository.AddAsync(detail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDetailAsync(CustomerQuoteDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                _customerQuoteDetailRepository.Update(detail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CustomerQuoteDetail> GetDetailByKeyAsync(string key)
        {
            return await _customerQuoteDetailRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.CustomerQuote));
        }

        public async Task<bool> DeleteDetailAsync(CustomerQuoteDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));
            try
            {
                _customerQuoteDetailRepository.Remove(detail);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<CustomerQuoteDetail>> GetAllDetailByQuoteAsync(CustomerQuote customerQuote)
        {
            return await _customerQuoteDetailRepository.GetAllAsync(
                predicate: x => x.CustomerQuote.Equals(customerQuote));
        }

        public async Task<int> GetCountAllByCustomerQuote(CustomerQuote quote)
        {
            return await _customerQuoteDetailRepository.CountAsync(
                predicate: x => x.CustomerQuote.Equals(quote));
        }
    }
}
