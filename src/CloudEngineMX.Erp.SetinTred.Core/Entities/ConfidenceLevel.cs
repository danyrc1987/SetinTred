namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    public class ConfidenceLevel : BaseEntity
    {
        public string ConfidenceLevelName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
