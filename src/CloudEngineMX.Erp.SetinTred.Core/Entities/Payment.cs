using System;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public decimal Ammount { get; set; }
        public string Reference { get; set; }
        public string Remarks { get; set; }
        public int PurcharseOrderId { get; set; }
        public int CurrencyId { get; set; }
        public int SupplierId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
