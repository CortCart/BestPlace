using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Category;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class QuestionnaireService:IQuestionnaireService
{
    private readonly IApplicatioDbRepository repository;

    public QuestionnaireService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<QuestionnaireListViewModel>> All()
    {
        var questionnaires = await this.repository.All<Questionnaire>()
            .Select(x => new QuestionnaireListViewModel
            {
                Id = x.Id,
                Name = x.Name

            }).ToListAsync();
        return  questionnaires;
    }

   
    public async Task<QuestionnaireDetailsViewModel> GetQuestionnaireDetails(Guid id)
    {
        var questionnaire = await this.repository.GetByIdAsync<Questionnaire>(id);


        return new QuestionnaireDetailsViewModel
        {
            Id = questionnaire.Id,
            Name = questionnaire.Name,
            Description = questionnaire.Description,
            Submits = questionnaire.SubmitQuestionnaires.Select(x => new QuestionnaireSubmitViewModel
            {
                Answer = x.Answer,
                UserId = x.UserId,
                UserName = $"{x.User.FirstName} {x.User.LastName}"
            }).ToList()
        };
    }

    public async Task<bool> AddQuestionnaire(QuestionnaireAddViewModel model)
    {
       
       try
       {
           var questionnaire = new Questionnaire
           {
               Name = model.Name,
               Description = model.Description
           };
           await this.repository.AddAsync(questionnaire);
           await this.repository.SaveChangesAsync();
            return true;
       }
       catch
       {

           return false;
       }
    }
}