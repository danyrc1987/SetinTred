using System;
using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public DateTime CreationDate { get; set; }
        public string PurchaseOrderKey { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string RequisitionCode { get; set; }
        public string UserName { get; set; }
        public string Applicant { get; set; }
        public string OperatingBaseName { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public string SendTo { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public bool WithApprovedDetails { get; set; }
        public bool RequiresVat { get; set; }
        public string TotalInLetters { get; set; }
        public string CancelComments { get; set; }
        public string KeyDescription { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Subtotal { get; set; }

        public IEnumerable<PurchaseOrderDetailViewModel> Details { get; set; }
        public IEnumerable<PurchaseOrderQuotationViewModel> Quotations { get; set; }
    }
}
