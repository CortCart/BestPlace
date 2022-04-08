using BestPlace.Core.Models;
using BestPlace.Core.Models.Admin;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Models.Questionnaire;

namespace BestPlace.Core.Contracts;

public interface IQuestionnaireService
{
    Task<IEnumerable<QuestionnaireListViewModel>> GetAllQuestionnairies();

    Task<IEnumerable<QuestionnaireUserListViewModel>> GetAllQuestionnairiesForUser(string userId);

    Task<QuestionnaireDetailsViewModel> GetQuestionnaireDetails(Guid id);

    Task<QuestionnaireDetailsViewModelAsAdmin> GetQuestionnaireDetailsAsAdmin(Guid id);

    Task<bool> AddQuestionnaire(QuestionnaireAddViewModel model, DateTime date);

    Task<bool> AddSubmit(SubmitQuestionnaireAddViewModel model, string userId, Guid questionnaireId);

}