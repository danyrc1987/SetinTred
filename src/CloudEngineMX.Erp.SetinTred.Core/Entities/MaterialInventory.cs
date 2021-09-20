namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System;

    public class MaterialInventory : BaseEntity
    {
        public string Code { get; set; }
        public string Serial { get; set; }
        public string Resolution { get; set; }
        public DateTime? DateOfCalibration { get; set; }
        public DateTime? DueDateCalibration { get; set; }
        public int? CalibrationFrequency { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public DateTime? InactiveSince { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public string CertificationNumber { get; set; }
        public string HeadOfVerification { get; set; }
        public string Remarks { get; set; }
        public int MaterialId { get; set; }

        public virtual MeasurementMaterial MeasurementMaterial { get; set; }
    }
}
