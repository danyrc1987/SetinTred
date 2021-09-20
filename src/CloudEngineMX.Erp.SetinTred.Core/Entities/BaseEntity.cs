namespace CloudEngineMX.Erp.SetinTred.Core.Entities
{
    using System;

    /// <summary>
    /// Configuration base entity
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
