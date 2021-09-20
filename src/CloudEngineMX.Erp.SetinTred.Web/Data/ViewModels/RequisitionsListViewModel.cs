namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RequisitionsListViewModel : ViewModelBase
    {
        public string CreationDate { get; set; }
        public string RequisitionCode { get; set; }
        public string AreaKey { get; set; }
        public int UserId { get; set; }
        public string OperatingBaseKey { get; set; }
        public string Classification { get; set; }
        public string Application { get; set; }
        public string CostCenter { get; set; }
        public string Status { get; set; }
        public string KeyDescription { get; set; }

        public IEnumerable<SelectListItem> OperatingBases { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }
        public IEnumerable<SelectListItem> Classifications { get; set; }
        public IEnumerable<RequisitionViewModel> Requisitions { get; set; }
        public IEnumerable<RequisitionViewModel> RequisitionsForApprove { get; set; }
        public IEnumerable<SelectListItem> CostCenters { get; set; }
    }
}
