using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    public class QuestionnaireController : BaseController
    {

        private readonly IQuestionnaireService questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            this.questionnaireService = questionnaireService;
        }

        public async Task<IActionResult> All()
        {
            var questionnaire =  await  this.questionnaireService.All();
            return View(questionnaire);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Add(QuestionnaireAddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await this.questionnaireService.AddQuestionnaire(model))
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

        public async Task<IActionResult> Details(Guid id)
        {
            var questionnaire = await this.questionnaireService.GetQuestionnaireDetails(id);
            return View(questionnaire);
        }
    }
}
