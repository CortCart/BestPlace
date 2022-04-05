using BestPlace.Core.Contracts;
using BestPlace.Infrastructure.Data;
using BestPlace.Models;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers;

public class DealController:BaseController
{

    private readonly IDealService dealService;

    public DealController(IDealService dealService)
    {
        this.dealService = dealService;
    }
    public async Task<IActionResult> All()
    {
        var deals = await this.dealService.All();
        return View(deals);
    }

    public async  Task<IActionResult> Details(Guid id)
    {
        try
        {
            var deal = await this.dealService.GetDealDetails(id);


            return View(deal);
        }
        catch
        {
            return View("Error", new ErrorViewModel() { name = "Unknown  deal" });
        }
    }
}