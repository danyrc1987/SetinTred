using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class RequisitionDetailViewModel
    {
        public RequisitionViewModel Requisition { get; set; }

        public IEnumerable<RequisitionDetailItemViewModel> Items { get; set; }

        public IEnumerable<RequisitionQuotationViewModel> Quotations { get; set; }
    }
}
