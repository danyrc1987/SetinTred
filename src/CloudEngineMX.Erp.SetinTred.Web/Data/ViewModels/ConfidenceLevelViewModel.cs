using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ConfidenceLevelViewModel : ViewModelBase
    {
        public string ConfidenceLevelName { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<ConfidenceLevelViewModel> ConfidenceLevelViewModels { get; set; }
    }
}
