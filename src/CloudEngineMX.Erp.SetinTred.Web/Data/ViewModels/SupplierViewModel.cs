namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// View model to supplier
    /// </summary>
    public class SupplierViewModel
    {
        public string SupplierKey { get; set; }
        public string FiscalName { get; set; }
        public string Rfc { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsCritical { get; set; }
        public string Services { get; set; }
        public string Specification { get; set; }
        public string SpecificationAvailability { get; set; }
        public string OperatingBaseKey { get; set; }
        public string ConfidenceLevelKey { get; set; }
        public DateTime NextScheduledEvaluation { get; set; }
        public string Remarks { get; set; }
        public string Action { get; set; }

        public List<SupplierViewModel> SupplierViewModels { get; set; }
        public IEnumerable<SelectListItem> OperatingBases { get; set; }
        public IEnumerable<SelectListItem> ConfidenceLevels { get; set; }

    }
}
