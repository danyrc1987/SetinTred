using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class CostCenterViewModel : ViewModelBase
    {
        public string CostCenterName { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<CostCenterViewModel> CostCenters { get; set; }
    }
}
