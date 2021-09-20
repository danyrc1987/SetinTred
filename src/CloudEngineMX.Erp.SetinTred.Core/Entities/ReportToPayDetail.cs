namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class ReportToPayDetail : BaseEntity
    {
        public string PurchaseOrderCode { get; set; }
        public string Applicant { get; set; }
        public string AreaName { get; set; }
        public decimal Total { get; set; }
        public string CurrencyName { get; set; }
        public string OperatingBaseName { get; set; }
        public string Application { get; set; }
        public string KeyDescription { get; set; }
        public string Classification { get; set; }
        public string FiscalName { get; set; }
        public bool IsCritical { get; set; }
    }
}
