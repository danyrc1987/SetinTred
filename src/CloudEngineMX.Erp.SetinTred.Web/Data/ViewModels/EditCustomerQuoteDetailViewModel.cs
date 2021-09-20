using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class EditCustomerQuoteDetailViewModel
    {
        public string QuoteKey { get; set; }
        public string QuoteCode { get; set; }
        public string CustomerName { get; set; }
        public string VendorName { get; set; }
        public string CurrencyName { get; set; }
        public string LegalDocumentation { get; set; }
        public string TechnicalData { get; set; }
        public string NormativeData { get; set; }
        public string ManufacturingStandard { get; set; }
        public string QualityProcess { get; set; }
        public string Validity { get; set; }
        public string QuoteType { get; set; }
        public string PaymentType { get; set; }
        public string Warranty { get; set; }
        public string DeliveryTime { get; set; }
        public string Status { get; set; }
        public string Lab { get; set; }
        public string CustomerId { get; set; }
        public string CurrencyKey { get; set; }

        public IEnumerable<ItemCustomerQuoteViewModel> Items { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
    }
}
