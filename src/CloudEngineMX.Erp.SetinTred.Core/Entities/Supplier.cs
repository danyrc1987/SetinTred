namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class Supplier : BaseEntity
    {
        public string FiscalName { get; set; }
        public bool IsCritical { get; set; }
        public string Services { get; set; }
        public string Specification { get; set; }
        public string SpecificationAvailability { get; set; }
        public int IdOperatingBase { get; set; }
        public int IdConfidenceLevel { get; set; }
        public DateTime NextScheduledEvaluation { get; set; }
        public string Remarks { get; set; }
        public string Rfc { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual OperatingBase OperatingBase { get; set; }
        public virtual ConfidenceLevel ConfidenceLevel { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
