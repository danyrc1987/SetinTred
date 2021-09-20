using System;
using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class PrintCustomerQuoteDetailViewModel
    {
        public string QuoteKey { get; set; }
        public string QuoteCode { get; set; }
        public DateTime CreationDate { get; set; }
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

        public IEnumerable<ItemCustomerQuoteViewModel> Items { get; set; }
    }
}
