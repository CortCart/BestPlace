using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async  Task<IActionResult> All()
        {
            var categories = await this.categoryService.All();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Add(CategoryAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await categoryService.AddCategory(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Add category";
                return RedirectToAction("All");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Error while add category";
            }

            return View(model);

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await this.categoryService.GetCategoryForEdit(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await categoryService.EditCategory(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Edit category";
                return RedirectToAction("All");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Error while edit category";
            }

            return View(model);
        }

        public async  Task<IActionResult> Remove(Guid id)
        {
           var category=  await  this.categoryService.DeleteCategory(id);
           if (category == false)
           {
               ViewData[MessageConstant.SuccessMessage] = "Error while delete category";
           }
           else
           {
               ViewData[MessageConstant.SuccessMessage] = "Category was deleted";
           }
           return RedirectToAction("All");
        }
    }
}
