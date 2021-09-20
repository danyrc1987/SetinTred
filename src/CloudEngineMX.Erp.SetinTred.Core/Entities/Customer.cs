using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string FiscalName { get; set; }
        public string SocialName { get; set; }
        public string Rfc { get; set; }
        public string PersonType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AdministrativeContactName { get; set; }
        public string AdministrativeContactEmail { get; set; }
        public string AdministrativeContactPhone { get; set; }
        public string FinancialContactName { get; set; }
        public string FinancialContactPhone { get; set; }
        public string FinancialContactEmail { get; set; }

        public ICollection<CustomerDocument> CustomerDocuments { get; set; }
        public ICollection<CustomerQuote> CustomerQuotes { get; set; }
    }
}
