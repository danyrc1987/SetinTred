namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PaymentViewModel
    {
        public DateTime PayentDate { get; set; }
        public string CurrencyKey { get; set; }
        public string PaymentType { get; set; }
        public decimal Ammount { get; set; }
        public string PurcharseOrderKey { get; set; }
        public string Reference { get; set; }
        public string Remarks { get; set; }
        public string SupplierName { get; set; }

        public IEnumerable<SelectListItem> Currencies { get; set; }
        public IEnumerable<SelectListItem> PurcharOrders { get; set; }

        public IEnumerable<PaymentsViewModel> Payments { get; set; }
    }
}
