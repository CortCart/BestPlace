using BestPlace.Core.Contracts;
using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestPlace.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userService.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await this.userService.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var users = await this.userService.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var info = await this.userService.GetUserDetails(id);

            return View(info);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var user = await this.userService.DeleteUser(id);
            if (user == false)
            {
                ViewData["Message"] = "Error while delete user";
            }
            else
            {
                ViewData["Message"] = "User was deleted";
            }
            return RedirectToAction("All");
        }

    }
}
