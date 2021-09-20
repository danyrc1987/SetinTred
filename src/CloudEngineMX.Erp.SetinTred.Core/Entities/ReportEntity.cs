namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class ReportEntity : BaseEntity
    {
        public string FiscalName { get; set; }
        public string Applicant { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
    }
}
