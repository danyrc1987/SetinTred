using System.Linq;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AccountController(
            IUserService userService,
            ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }


        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Infrastructure.Data.ViewModels.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _logger.Information($"Intento de login por el usuario: {model.Email}");

            var result = await _userService.LoginAsync(model);

            if (result.Succeeded)
            {

                _logger.Information($"Acceso correcto del usuario: {model.Email}");

                if (Request.Query.Keys.Contains("ReturnUrl"))
                {
                    return Redirect(url: Request.Query["ReturnUrl"].First());
                }

                return RedirectToAction("Index", "Home");
            }

            var response = result.IsNotAllowed ? "Acceso no confirmado" :
                result.IsLockedOut ? "Acceso Bloqueado" : "Usuario y/o contraseña incorrecta";

            return RedirectToAction("Login", "Account")
                .WithWarning("Operación Incorrecta", response);
        }

        public async Task<IActionResult> Logout()
        {
            await this._userService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
