using System;
using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        public string PurchaseOrderCode { get; set; }
        public string RequisitionCode { get; set; }
        public int UserId { get; set; }
        public string Applicant { get; set; }
        public int OperatingBaseId { get; set; }
        public int SupplierId { get; set; }
        public string Status { get; set; }
        public string SendTo { get; set; }
        public string DeliveryTime { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public bool RequiresVat { get; set; }
        public string TotalInLetters { get; set; }
        public string DigitalSignature { get; set; }
        public string CancelComments { get; set; }
        public int CurrencyId { get; set; }
        public string KeyDescription { get; set; }
        public DateTime? LiberationDate { get; set; }


        public virtual UserCore UserCore { get; set; }
        public virtual OperatingBase OperatingBase { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
