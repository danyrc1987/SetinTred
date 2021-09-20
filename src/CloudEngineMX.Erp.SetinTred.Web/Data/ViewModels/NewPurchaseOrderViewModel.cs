namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class NewPurchaseOrderViewModel
    {
        public string RequisitionCode { get; set; }
        public string Applicant { get; set; }
        public string OperatingBaseName { get; set; }
        public string SupplierName { get; set; }
        public string SendTo { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public string DeliveryTime { get; set; }
        public string Currency { get; set; }
        public bool VatRequired { get; set; }

        public IEnumerable<SelectListItem> Requisitions { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Bases { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }

    }
}
