namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IPurchaseOrderDetailService
    {
        Task<bool> AddPurchaseOrderDetailAsync(PurchaseOrderDetail detail);
        Task<bool> UpdatePurchaseOrderDetailAsync(PurchaseOrderDetail detail);
        Task<IEnumerable<PurchaseOrderDetail>> GetAllItemsByOrderAsync(PurchaseOrder order);
        Task<PurchaseOrderDetail> GetDetailByKeyAsync(string key);
        Task<int> GetTotalItemsCountAsync(PurchaseOrder order);
        Task<bool> DeleteItemAsync(PurchaseOrderDetail detail);
    }
}
