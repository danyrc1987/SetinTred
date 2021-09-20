using System;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class CustomerDocument : BaseEntity
    {
        public DateTime UpdateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentRoute { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
