using System.Globalization;
using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstants.Roles.QuestionnaireViewer)]

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
                if (!ModelState.IsValid)
                {
                    foreach (var errors in ModelState.Values)
                    {
                        foreach (var error in errors.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        }
                    }

                    return View(model);
                }
            }

           

            DateTime date;
        bool isTrue =DateTime.TryParseExact(model.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        if (!isTrue)
        {
            ModelState.AddModelError(string.Empty, "Error while adding questionnaire");
            return View(model);
        }

        if (!await this.questionnaireService.AddQuestionnaire(model, date))
        {
            ModelState.AddModelError(string.Empty, "Error while adding questionnaire");
            return View(model);

        }


            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var questionnaire = await this.questionnaireService.GetQuestionnaireDetailsAsAdmin(id);

                return View(questionnaire);
            }
            catch
            {
                return View("Error", new ErrorViewModel() { name = "Unknown  questionnaire" });
            }
        }
    }
}
