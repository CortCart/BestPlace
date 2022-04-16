using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstants.Roles.ItemViewer)]

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
            try
            {
                await this.itemService.DeleteItemAsAdmin(id);

                return RedirectToAction("All");
            }
            catch 
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  item" });

            }

        }

        public async Task<IActionResult> Details( Guid id)
        {
            try
            {
                var item = await this.itemService.GetItemDetailsAsAdmin(id);
                return View(item);
            }
            catch 
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  item" });

            }

        }

    }
}
