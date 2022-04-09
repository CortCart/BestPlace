using BestPlace.Core.Constants;
using BestPlace.Core.Models.Call;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Controllers
{
    [Authorize]
    public class CallController : Controller
    {
        private readonly ICallService callService;
        private  readonly  UserManager<ApplicationUser> userManager;


        public CallController(ICallService callService , UserManager<ApplicationUser> userManager)
        {
            this.callService = callService;
            this.userManager = userManager;
        }

        public  async Task<IActionResult> Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(CallAddViewModel model)
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

                return View();
            }

            if (!await this.callService.AddCall(model, this.userManager.GetUserId(User)))
            {
                ModelState.AddModelError(string.Empty, "Error while add call");
                return View();
            }

            return Redirect("/");
        }
    }
}
