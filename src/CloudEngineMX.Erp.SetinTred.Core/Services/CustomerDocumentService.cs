using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class CustomerDocumentService : ICustomerDocumentService
    {
        private readonly IRepository<CustomerDocument> _customerDocument;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerDocumentService(
            IRepository<CustomerDocument> customerDocument,
            IUnitOfWork unitOfWork)
        {
            _customerDocument = customerDocument;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateDocumentAsync(CustomerDocument customerDocument)
        {
            if (customerDocument == null)
                throw new ArgumentNullException(nameof(customerDocument));
            try
            {
                await _customerDocument.AddAsync(customerDocument);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateListDocumentAsync(List<CustomerDocument> customerDocuments)
        {
            if (customerDocuments == null)
                throw new ArgumentNullException(nameof(customerDocuments));
            try
            {
                await _customerDocument.AddAsync(customerDocuments);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveDocumentAsync(CustomerDocument customerDocument)
        {
            if (customerDocument == null)
                throw new ArgumentNullException(nameof(customerDocument));
            try
            {
                _customerDocument.Remove(customerDocument);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CustomerDocument> GetDocumentByIdAsync(int id)
        {
            return await _customerDocument.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }
    }
}
