namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    public class OperatingBase : BaseEntity
    {
        public string OperatingBaseName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
