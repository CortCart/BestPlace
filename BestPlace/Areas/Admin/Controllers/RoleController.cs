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
            if (!ModelState.IsValid)
            {
                foreach (var errors in ModelState.Values)
                {
                    foreach (var error in errors.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }

                return View(model);
            }

            if (!await this.roleService.AddRole(model.Name))
            {
                ModelState.AddModelError(string.Empty,"Error while add role");

            }

            return RedirectToAction(nameof(All));
        }
    }
}
