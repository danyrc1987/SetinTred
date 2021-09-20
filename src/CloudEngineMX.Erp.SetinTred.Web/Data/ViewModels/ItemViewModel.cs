using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ItemViewModel
    {
        public int Quantity { get; set; }
        public string Measure { get; set; }
        public string Description { get; set; }
        public string SpecificationKey { get; set; }
        public string RequisitionKey { get; set; }
        public bool IsUrgent { get; set; }

        public IEnumerable<SelectListItem> Specifications { get; set; }
    }
}
