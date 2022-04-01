using BestPlace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
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
