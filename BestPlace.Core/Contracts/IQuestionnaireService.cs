using BestPlace.Core.Models;

namespace BestPlace.Core.Contracts;

public interface IQuestionnaireService
{
    Task<IEnumerable<QuestionnaireListViewModel>> GetAllQuestionnairies();

    Task<QuestionnaireDetailsViewModel> GetQuestionnaireDetails(Guid id);

    Task<bool> AddQuestionnaire(QuestionnaireAddViewModel model);
}