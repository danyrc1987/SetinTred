namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class CustomerQuoteDetail : BaseEntity
    {
        public int Consecutive { get; set; }
        public string TypeItem { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Availability { get; set; }
        public string Scope { get; set; }
        public string Remarks { get; set; }
        public decimal UnitPrice { get; set; }
        public int CustomerQuoteId { get; set; }

        public CustomerQuote CustomerQuote { get; set; }
    }
}
