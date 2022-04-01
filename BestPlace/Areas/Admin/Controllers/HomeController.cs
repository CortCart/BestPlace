using BestPlace.Core.Contracts;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IStatisticsService statisticsService;

        public HomeController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IStatisticsService statisticsService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.statisticsService = statisticsService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var statistics = await this.statisticsService.GetCounts();
            return View(statistics);
        }
    }
}
