using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class SpecificationTypeViewModel : ViewModelBase
    {
        public string SpecificationTypeName { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<SpecificationTypeViewModel> SpecificationTypeViewModels { get; set; }
    }
}
