namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class Quotation : BaseEntity
    {
        public string RouteFile { get; set; }
        public string FileName { get; set; }
        public int PurchaseOrderId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

    }
}
