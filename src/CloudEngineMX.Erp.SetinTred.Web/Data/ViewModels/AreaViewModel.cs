namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;

    public class AreaViewModel
    {
        public string Action { get; set; }

        public string AreaKey { get; set; }

        public string AreaName { get; set; }

        public IEnumerable<AreaViewModel> AreaViewModels { get; set; }
    }
}
