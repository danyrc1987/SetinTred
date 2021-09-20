using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class CustomerQuote : BaseEntity
    {
        public string QuoteCode { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string LegalDocumentation { get; set; }
        public string TechnicalData { get; set; }
        public string NormativeData { get; set; }
        public string ManufacturingStandard { get; set; }
        public string QualityProcess { get; set; }
        public int CurrencyId { get; set; }
        public string Status { get; set; }
        public string Validity { get; set; }
        public string QuoteType { get; set; }
        public string PaymentType { get; set; }
        public string Warranty { get; set; }
        public string DeliveryTime { get; set; }
        public string Lab { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual UserCore User { get; set; }
        public virtual Currency Currency { get; set; }
        public ICollection<CustomerQuoteDetail> CustomerQuoteDetails { get; set; }
        public ICollection<CustomerQuoteLog> CustomerQuoteLogs { get; set; }
    }
}
