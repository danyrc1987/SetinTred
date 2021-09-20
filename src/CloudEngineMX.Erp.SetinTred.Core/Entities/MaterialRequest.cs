namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class MaterialRequest : BaseEntity
    {
        public string RequestCode { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsFinished { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public DateTime? EntryDate { get; set; }

        public virtual UserCore UserCore { get; set; }
        public virtual ICollection<MaterialRequestDetail> MaterialRequestDetails { get; set; }
    }
}
