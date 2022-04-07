using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Roles;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private  readonly  IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<IActionResult> All()
        {
            var roles = await this.roleService.GetAllRoles();
            return View(roles);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleAddViewModel model)
        {
            await this.roleService.AddRole(model.Name);
            return RedirectToAction("All");
        }
    }
}
