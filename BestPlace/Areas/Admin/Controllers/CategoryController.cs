using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Category;
using BestPlace.Models;
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

            await categoryService.AddCategory(model);
            
                return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = await this.categoryService.GetCategoryForEdit(id);
                return View(model);
            }
            catch 
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  category" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.EditCategory(model);
          

            return View(model);
        }

        public async  Task<IActionResult> Remove(Guid id)
        {
          
          
           try
           {
             await this.categoryService.DeleteCategory(id);
               return RedirectToAction("All");
            }
           catch
           {
               return View("Error", new ErrorViewModel() { name = "Unknown  category" });
           }

           
        }
    }
}
