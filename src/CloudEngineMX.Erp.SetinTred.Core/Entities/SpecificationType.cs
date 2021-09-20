using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class SpecificationType : BaseEntity
    {
        public string SpecificationTypeName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Specification> Specifications { get; set; }
    }
}
