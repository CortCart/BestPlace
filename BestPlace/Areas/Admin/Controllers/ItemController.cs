using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService )
        {
            this.itemService= itemService;
        }

        public async Task<IActionResult> All()
        {
            var items = await this.itemService.All();
            return View(items);
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await this.itemService.DeleteItem(id);
            if (user == false)
            {
                ViewData[MessageConstant.SuccessMessage] = "Error while delete item";
            }
            else
            {
                ViewData[MessageConstant.SuccessMessage] = "Item was deleted";
            }
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details( Guid id)
        {
            var item = await this.itemService.GetItemDetails(id);
            return View(item);
        }

    }
}
