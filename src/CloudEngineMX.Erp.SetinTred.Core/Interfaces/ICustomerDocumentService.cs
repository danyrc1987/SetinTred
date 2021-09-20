using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface ICustomerDocumentService
    {
        Task<bool> CreateDocumentAsync(CustomerDocument customerDocument);
        Task<bool> CreateListDocumentAsync(List<CustomerDocument> customerDocuments);
        Task<bool> RemoveDocumentAsync(CustomerDocument customerDocument);
        Task<CustomerDocument> GetDocumentByIdAsync(int id);

    }
}
