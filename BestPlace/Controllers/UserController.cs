using BestPlace.Core.Contracts;
using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService= userService;
            this.userManager = userManager;
        }
        public async  Task<IActionResult> Info(string id)
        {
            var info = await this.userService.GetUserDetails(id);
            return View(info);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var info = await this.userService.GetUserForEdit(id);
            return View(info);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                foreach (var errors in ModelState.Values)
                {
                    foreach (var error in errors.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }

                return View();
            }


            if (!await this.userService.EditUser(model, this.userManager.GetUserId(User)))
            {
                ModelState.AddModelError(string.Empty, "Error while edit user");
                return View();
            }

            return RedirectToAction("Info", new
            {
                id = this.userManager.GetUserId(User)
            });
        }
    }
}
