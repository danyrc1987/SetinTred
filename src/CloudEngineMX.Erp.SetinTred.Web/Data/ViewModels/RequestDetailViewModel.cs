using System.Linq;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class RequestDetailViewModel
    {
        public string RequestKey { get; set; }
        public string RequestCode { get; set; }
        public string UserName { get; set; }
        public string AreaName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsEntry { get; set; }

        public int TotalItems => RequisitionDetailItemViewModels.Count();

        public IEnumerable<RequestDetailItemViewModel> RequisitionDetailItemViewModels { get; set; }
    }
}
