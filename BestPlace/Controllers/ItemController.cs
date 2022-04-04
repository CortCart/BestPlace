using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Item;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestPlace.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly  ICategoryService categoryService;
        private readonly IItemService itemService;
        private readonly UserManager<ApplicationUser> userManager;


        public ItemController(ICategoryService categoryService, IItemService itemService, UserManager<ApplicationUser> userManager)
        {
            this.categoryService = categoryService;
            this.itemService = itemService;
            this.userManager = userManager;

        }

        public async  Task<IActionResult> Add()
        {
            var categories = await this.categoryService.All();
               
            var categoriesForView = categories
                .Select(r => new SelectListItem()
              {
                  Text = r.Name,
                  Value = r.Id.ToString(),
              }).ToList();
            ViewBag.Categories= categoriesForView;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var errors in ModelState.Values)
                {
                    foreach (var error in  errors.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
                return View();
            }

            
            await  this.itemService.Add(model, this.userManager.GetUserId(User));

            return RedirectToAction("All");
        }
    }
}
