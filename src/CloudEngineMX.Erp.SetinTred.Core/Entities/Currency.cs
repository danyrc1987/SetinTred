using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class Currency : BaseEntity
    {
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ExchangeRate { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public ICollection<CustomerQuote> CustomerQuotes { get; set; }
    }
}
