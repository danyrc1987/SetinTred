namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class MaterialViewModel
    {
        public string MaterialKey { get; set; }
        public string ManufacturerName { get; set; }
        public string MaterialName { get; set; }
        public bool IsActive { get; set; }
        public int Total => !Inventories.Any() ? 0 : Inventories.Count();

        public IEnumerable<MaterialInventoryViewModel> Inventories { get; set; }

    }
}
