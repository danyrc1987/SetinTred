using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            try
            {
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            try
            {
                _customerRepository.Update(customer);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllCustomerComboAsync()
        {
            var data = await _customerRepository.GetAllAsync();

            var lst = data.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SocialName + " - " + x.Rfc
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Selecciona una Opción]",
                Value = string.Empty
            });

            return lst;
        }
    }
}
