namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;

    public class OperatingBaseViewModel : ViewModelBase
    {
        public string OperatingBaseName { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<OperatingBaseViewModel> OperatingBaseViewModels { get; set; }
    }
}
