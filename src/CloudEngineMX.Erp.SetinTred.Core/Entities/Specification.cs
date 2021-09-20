namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    public class Specification : BaseEntity
    {
        public string SpecificationName { get; set; }

        public int IdSpecificationType { get; set; }

        public int Revision { get; set; }

        public bool IsActive { get; set; }

        public virtual SpecificationType SpecificationType { get; set; }
    }
}
