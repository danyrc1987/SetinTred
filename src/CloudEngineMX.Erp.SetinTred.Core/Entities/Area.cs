namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Areas of company
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class Area : BaseEntity
    {
        public string AreaName { get; set; }

        public virtual ICollection<UserCore> UserCores { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
    }
}
