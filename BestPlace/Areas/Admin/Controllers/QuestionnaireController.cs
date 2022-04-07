using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Models;
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
            var questionnaire =  await  this.questionnaireService.GetAllQuestionnairies();
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

         await   this.questionnaireService.AddQuestionnaire(model);
           

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var questionnaire = await this.questionnaireService.GetQuestionnaireDetails(id);

                return View(questionnaire);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  questionnaire" });
            }
        }
    }
}
