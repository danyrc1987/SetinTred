using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ReportToPayViewModel
    {
        public string SupplierName { get; set; }
        public int TotalPurchase { get; set; }
        public List<PurchaseOrderViewModel> PurchaseOrderViewModels { get; set; }

        public List<PurchaseOrderDetailViewModel> Details { get; set; }
    }
}
