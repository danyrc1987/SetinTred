using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> CreatePaymentAsync(Payment payment);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<IEnumerable<Payment>> GetAllPaymentsByCurrencyAsync(Currency currency);
        Task<Payment> GetPaymentByKeyAsync(string key);
        Task<IEnumerable<Payment>> GetAllPaymentsBySupplier(Supplier supplier);
        Task<IEnumerable<Payment>> GetAllPaymentsByDate(DateTime initialDate, DateTime endaDate);
    }
}
