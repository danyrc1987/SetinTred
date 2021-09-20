namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class RequisitionDetail : BaseEntity
    {
        public int RequisitionId { get; set; }
        public int Consecutive { get; set; }
        public int Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Review { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsApproved { get; set; }
        public string ReasonForRejection { get; set; }
        public int? QuantityDispatched { get; set; }
        public int? QuantityToBuy { get; set; }

        public virtual Requisition Requisition { get; set; }
    }
}
