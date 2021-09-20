namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class PreQuotation : BaseEntity
    {
        public string RouteFile { get; set; }
        public string FileName { get; set; }
        public int RequisitionId { get; set; }

        public virtual Requisition Requisition { get; set; }
    }
}
