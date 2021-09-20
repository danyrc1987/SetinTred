using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        public IActionResult ComingSoon()
        {
            return View();
        }
    }
}
