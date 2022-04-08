using BestPlace.Areas.Admin.Controllers;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Models.Questionnaire;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Controllers;

public class QuestionnaireController:Controller
{
    private readonly IQuestionnaireService questionnaireService;
    private readonly UserManager<ApplicationUser> userManager;

    public QuestionnaireController(IQuestionnaireService questionnaireService, UserManager<ApplicationUser> userManager)
    {
        this.questionnaireService=questionnaireService;
        this.userManager=userManager;
    }

    public async Task<IActionResult> All()
    {
        var questionnaire = await this.questionnaireService.GetAllQuestionnairiesForUser(this.userManager.GetUserId(User));
        return View(questionnaire);
    }
    [HttpGet]
    public async Task<IActionResult> Submit(Guid id)
    {
        var questionnaire = await this.questionnaireService.GetQuestionnaireDetails(id);
        ViewBag.Name = questionnaire.Name;
        ViewBag.Description = questionnaire.Description;
        ViewBag.id = id;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Submit(SubmitQuestionnaireAddViewModel model, Guid id)
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

      
       
            var submit = await this.questionnaireService.AddSubmit(model, this.userManager.GetUserId(User), id);

            return Redirect("/");
       

    }
}