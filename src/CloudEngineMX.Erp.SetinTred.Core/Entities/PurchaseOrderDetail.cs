namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class PurchaseOrderDetail : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public int Consecutive { get; set; }
        public int Quantity { get; set; }
        public int? OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsApproved { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

    }
}
