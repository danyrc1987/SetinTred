using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(
            IRepository<Payment> paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException();
            try
            {
                await _paymentRepository.AddAsync(payment);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException();
            try
            {
                _paymentRepository.Update(payment);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllAsync(
                include: i => i.Include(x => x.Supplier)
                     .Include(x => x.Currency)
                     .Include(x => x.PurchaseOrder));
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsByCurrencyAsync(Currency currency)
        {
            return await _paymentRepository.GetAllAsync(
                predicate: x => x.Currency.Equals(currency),
                include: i => i.Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.PurchaseOrder));
        }

        public async Task<Payment> GetPaymentByKeyAsync(string key)
        {
            return await _paymentRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.PurchaseOrder));
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsBySupplier(Supplier supplier)
        {
            return await _paymentRepository.GetAllAsync(
                predicate: x => x.Supplier.Equals(supplier),
                include: i => i.Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.PurchaseOrder));
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsByDate(DateTime initialDate, DateTime endaDate)
        {
            return await _paymentRepository.GetAllAsync(
                predicate: x => x.PaymentDate >= initialDate && x.PaymentDate <= endaDate,
                include: i => i.Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.PurchaseOrder));
        }
    }
}
