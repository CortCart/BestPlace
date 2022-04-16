using BestPlace.Core.Constants;
using BestPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstants.Roles.CallViewer)]

    public class CallController : BaseController
    {
        private readonly ICallService callService;

        public CallController(ICallService callService)
        {
            this.callService = callService;
        }
        public async Task<IActionResult> All()
        {
            var all = await this.callService.All();
            return View(all);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var details = await this.callService.GetCallDetails(id);
                return View(details);
            }
            catch 
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  call" });

            }
        }
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                 await this.callService.DeleteCall(id);
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  call" });

            }
        }
    }
}
