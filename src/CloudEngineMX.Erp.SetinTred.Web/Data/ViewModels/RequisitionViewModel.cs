using System;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class RequisitionViewModel
    {
        public DateTime CreationDate { get; set; }
        public string RequisitionKey { get; set; }
        public string RequisitionCode { get; set; }
        public string AreaName { get; set; }
        public string UserName { get; set; }
        public string OperatingBaseName { get; set; }
        public string Classification { get; set; }
        public string Application { get; set; }
        public string CostCenter { get; set; }
        public string Status { get; set; }
        public string KeyDescription { get; set; }
    }
}
