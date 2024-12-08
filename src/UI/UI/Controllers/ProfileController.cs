using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using UI.Models;
using UI.ViewModels;

namespace UI.Controllers
{
    public class ProfileController(IProfileService profileService) : Controller
    {
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var profile = await profileService.GetProfile(User.Identity?.Name!);
            return View(profile);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UserProfile(ProfileDto profile)
        {
            try
            { 
                await profileService.UpdateProfile(profile, User.Identity?.Name!);  
                return View(profile);
            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel { RequestId = $"Error  =  {ex.Message}"});
            }
        }

        [Authorize]
        [HttpGet]

        public IActionResult UpdatePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdatePassword(PasswordUpdateVM passwordUpdateVM)
        {
            try
            {
                await profileService.UpdatePassword(User.Identity?.Name!, passwordUpdateVM.OldPassword, passwordUpdateVM.newPassword);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error",ex.Message);
            }
            return View(passwordUpdateVM);
        }

    }
}
