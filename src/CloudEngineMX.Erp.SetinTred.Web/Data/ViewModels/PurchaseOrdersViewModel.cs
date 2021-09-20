namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;

    public class PurchaseOrdersViewModel
    {
        public IEnumerable<RequisitionViewModel> Requisitions { get; set; }

        public IEnumerable<PurchaseOrderViewModel> Orders { get; set; }
    }
}
