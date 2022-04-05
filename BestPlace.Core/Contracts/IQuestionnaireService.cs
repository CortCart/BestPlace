using BestPlace.Core.Models;

namespace BestPlace.Core.Contracts;

public interface IQuestionnaireService
{
    Task<IEnumerable<QuestionnaireListViewModel>> All();

    Task<QuestionnaireDetailsViewModel> GetQuestionnaireDetails(Guid id);

    Task AddQuestionnaire(QuestionnaireAddViewModel model);
}