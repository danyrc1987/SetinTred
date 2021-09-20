using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    /// <summary>
    /// User Core Object
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class UserCore : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int ReportId { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public UserCore Report { get; set; }
        public Area Area { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
        public virtual ICollection<MaterialRequest> MaterialRequests { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<CustomerQuote> CustomerQuotes { get; set; }

        public ICollection<CustomerQuoteLog> CustomerQuoteLogs { get; set; }

    }
}
