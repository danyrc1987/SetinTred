using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Action { get; set; }

        public string UserKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string AreaKey { get; set; }

        public string UserReportKey { get; set; }

        public string Email { get; set; }

        public string AreaName { get; set; }

        public bool IsAdministrator { get; set; }

        public IEnumerable<SelectListItem> Areas { get; set; }
        public IEnumerable<SelectListItem> UserReports { get; set; }

        public IEnumerable<UserViewModel> UserViewModels { get; set; }

    }
}
