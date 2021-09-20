namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class MaterialRequestDetail : BaseEntity
    {
        public int MeasurementMaterialId { get; set; }
        public int MaterialRequestId { get; set; }
        public int Quantity { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsEntry { get; set; }

        public virtual MeasurementMaterial Material { get; set; }
        public virtual MaterialRequest MaterialRequest { get; set; }

    }
}
