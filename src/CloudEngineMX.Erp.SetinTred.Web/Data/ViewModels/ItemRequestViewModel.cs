using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ItemRequestViewModel
    {
        public string RequestKey { get; set; }
        public int Quantity { get; set; }
        public string MaterialKey { get; set; }
        public IEnumerable<SelectListItem> Materials { get; set; }

        public IEnumerable<RequestDetailItemViewModel> Items { get; set; }

    }
}
