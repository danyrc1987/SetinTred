namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    public class Requisition : BaseEntity
    {
        public string RequisitionCode { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
        public int OperatingBaseId { get; set; }
        public string Classification { get; set; }
        public string CostCenter { get; set; }
        public string Application { get; set; }
        public string Status { get; set; }
        public string KeyDescription { get; set; }

        public virtual Area Area { get; set; }
        public virtual UserCore UserCore { get; set; }
        public virtual OperatingBase OperatingBase { get; set; }
        public virtual ICollection<RequisitionDetail> RequisitionDetails { get; set; }
        public virtual ICollection<PreQuotation> PreQuotations { get; set; }
    }
}
