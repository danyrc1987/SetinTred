namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class CustomerQuoteLog : BaseEntity
    {
        public int UserId { get; set; }
        public int QuoteId { get; set; }
        public string Movement { get; set; }
        public string Description { get; set; }

        public UserCore User { get; set; }
        public CustomerQuote CustomerQuote { get; set; }
    }
}
