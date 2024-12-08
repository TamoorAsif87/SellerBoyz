using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.DTO;

namespace UI.Controllers
{
    public class AuthController(IAuth authService) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                await authService.RegisterUer(registerDto);
                return RedirectToAction(controllerName:"auth",actionName:"login");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", ex.Message);
            }

            return View(registerDto);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                await authService.LoginUser(loginDto);
                return RedirectToAction(controllerName: "home", actionName: "index");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", ex.Message);
            }

            return View(loginDto);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();
            return RedirectToAction(controllerName: "home", actionName: "index");
        }
    }
}
