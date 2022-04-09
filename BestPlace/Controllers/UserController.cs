using BestPlace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService= userService;
        }
        public async  Task<IActionResult> Info(string id)
        {
            var info = await this.userService.GetUserDetails(id);
            return View(info);
        }
    }
}
