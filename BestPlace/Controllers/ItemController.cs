using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Item;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestPlace.Controllers
{
  
    public class ItemController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IItemService itemService;
        private readonly UserManager<ApplicationUser> userManager;


        public ItemController(ICategoryService categoryService, IItemService itemService, UserManager<ApplicationUser> userManager)
        {
            this.categoryService = categoryService;
            this.itemService = itemService;
            this.userManager = userManager;

        }

        public async Task<IActionResult> All(Guid categoryId, string query)
        {
            var items = await this.itemService.AllPublic(categoryId, query);
            var categories = await this.categoryService.All();

            var categoriesForView = categories
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                    Selected = r.Id == categoryId
                }).ToList();
            ViewBag.Categories = categoriesForView;
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var categories = await this.categoryService.All();
                var item = await this.itemService.GetItemForEdit(id, this.userManager.GetUserId(User));
                var categoriesForView = categories
                    .Select(r => new SelectListItem()
                    {
                        Text = r.Name,
                        Value = r.Id.ToString(),
                        Selected = r.Id == Guid.Parse(item.CategoryId)
                    }).ToList();
                ViewBag.Categories = categoriesForView;

                return View(item);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  item" });

            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await this.categoryService.All();

                foreach (var errors in ModelState.Values)
                {
                    foreach (var error in errors.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    try
                    {
                        var item = await this.itemService.GetItemForEdit(model.Id, this.userManager.GetUserId(User));

                    }
                    catch
                    {
                        return View("Error", new ErrorViewModel() {name = "Unknown  item"});

                    }
                }
                var categoriesForView = categories
                    .Select(r => new SelectListItem()
                    {
                        Text = r.Name,
                        Value = r.Id.ToString(),
                        Selected = r.Id == Guid.Parse(model.CategoryId)
                    }).ToList();
                ViewBag.Categories = categoriesForView;
                return RedirectToAction("Edit", model.Id);
            }


            await this.itemService.Edit(model, this.userManager.GetUserId(User));

            return RedirectToAction("All", new { categoryId = Guid.Parse(model.CategoryId), query = "" });
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.categoryService.All();

            var categoriesForView = categories
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }).ToList();
            ViewBag.Categories = categoriesForView;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemAddViewModel model)
        {
            var categories = await this.categoryService.All();

            var categoriesForView = categories
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }).ToList();
            ViewBag.Categories = categoriesForView;
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


            if (!await this.itemService.AddItem(model, this.userManager.GetUserId(User)))
            {
                ModelState.AddModelError(string.Empty, "Error while add item");
                return View();
            }

            return RedirectToAction("All", new
            {
                 categoryId= model.CategoryId,
                 query = ""
            });
        }

        public async Task<IActionResult> Details(Guid id)
        {

            try
            {
                var item = await this.itemService.GetItemDetails(id);
                return View(item);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  item" });

            }
          
        }
    }
}
