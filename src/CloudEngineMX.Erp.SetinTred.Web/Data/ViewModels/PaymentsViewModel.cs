using System;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class PaymentsViewModel
    {
        public string PaymentKey { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string SupplierName { get; set; }
        public string PurchaseOrderCode { get; set; }
        public decimal Ammount { get; set; }
        public string CurrencyName { get; set; }
    }
}
