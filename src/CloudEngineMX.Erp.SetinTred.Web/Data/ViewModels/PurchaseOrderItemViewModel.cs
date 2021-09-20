using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class PurchaseOrderItemViewModel
    {
        public string PurchaseOrderKey { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        public string ItemKey { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
