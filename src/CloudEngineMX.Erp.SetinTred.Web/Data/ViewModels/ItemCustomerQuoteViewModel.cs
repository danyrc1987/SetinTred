namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ItemCustomerQuoteViewModel
    {
        public string Key { get; set; }
        public int Consecutive { get; set; }
        public string TypeItem { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Availability { get; set; }
        public decimal UnitPrice { get; set; }
        public int CustomerQuoteId { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
        public string Scope { get; set; }
        public string Remarks { get; set; }
    }
}
