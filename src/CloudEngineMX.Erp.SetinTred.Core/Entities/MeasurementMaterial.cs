namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    public class MeasurementMaterial : BaseEntity
    {
        public string MaterialName { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsActive { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<MaterialInventory> MaterialInventories { get; set; }
        public virtual ICollection<MaterialRequestDetail> MaterialRequestDetails { get; set; }
    }
}
