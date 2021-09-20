namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    public class Manufacturer : BaseEntity
    {
        public string ManufacturerName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MeasurementMaterial> MeasurementMaterials { get; set; }
    }
}
