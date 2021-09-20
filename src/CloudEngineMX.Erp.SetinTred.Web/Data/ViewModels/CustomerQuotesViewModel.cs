using System.Collections.Generic;
using System.Linq;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class CustomerQuotesViewModel
    {
        public string QuoteKey { get; set; }
        public string QuoteCode { get; set; }
        public string CreationDate { get; set; }
        public string CustomerName { get; set; }
        public string CurrencyName { get; set; }
        public string Status { get; set; }
        public string QuoteType { get; set; }
        public string PaymentType { get; set; }
        public string DeliveryTime { get; set; }
        public int TotalItems => Details.Count();
        public decimal Subtotal => Details.Sum(x => x.Quantity * x.UnitPrice);

        public IEnumerable<CustomerQuotesViewModel> Quotes { get; set; }
        public IEnumerable<ItemCustomerQuoteViewModel> Details { get; set; }
    }
}
