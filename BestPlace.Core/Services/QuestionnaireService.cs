using System.Globalization;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Admin;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Models.Questionnaire;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
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

    public async Task<IEnumerable<QuestionnaireListViewModel>> GetAllQuestionnairies()
    {
        var questionnaires = await this.repository.All<Questionnaire>()
            .Select(x => new QuestionnaireListViewModel
            {
                Id = x.Id,
                Name = x.Name

            }).ToListAsync();
        return  questionnaires;
    }
    public async Task<IEnumerable<QuestionnaireUserListViewModel>> GetAllQuestionnairiesForUser(string userId)
    {
        var user =await this.repository.GetByIdAsync<ApplicationUser>(userId);
        if (user == null) throw new ArgumentException("Unknown  user");
        return  await this.repository.All<Questionnaire>()
            .Where(x=>x.SubmitQuestionnaires.All(x=>x.UserId!=userId))
            .Where(x=>x.DueDate>DateTime.Now.Date)
            .Select(x => new QuestionnaireUserListViewModel
            {
                Id = x.Id,
                Name = x.Name

            }).ToListAsync();
         }

    public async Task<QuestionnaireDetailsViewModelAsAdmin> GetQuestionnaireDetailsAsAdmin(Guid id)
    {
        var questionnaire = await this.repository.All<Questionnaire>().Include(x=>x.SubmitQuestionnaires).FirstOrDefaultAsync(x=>x.Id==id);

        if (questionnaire == null) throw new ArgumentException("Unknown  questionnaire");

        var list = questionnaire.SubmitQuestionnaires.Select(x =>
             this.repository.All<SubmitQuestionnaire>().Include(y => y.User)
                .FirstOrDefault(y => y.Id == x.Id));
        
            return new QuestionnaireDetailsViewModelAsAdmin
            {
                Id = questionnaire.Id,
                Name = questionnaire.Name,
                Description = questionnaire.Description,
                Submits =list.Select(x => new QuestionnaireSubmitViewModel
                {
                    Answer = x.Answer,
                    UserId = x.UserId,
                    UserName = $"{x.User.FirstName} {x.User.LastName}"
                }).ToList()
            };
    }

    public async Task<QuestionnaireDetailsViewModel> GetQuestionnaireDetails(Guid id)
    {
        var questionnaire = await this.repository.GetByIdAsync<Questionnaire>(id);

        if (questionnaire == null) throw new ArgumentException("Unknown  questionnaire");


        return new QuestionnaireDetailsViewModel
        {
            Id = questionnaire.Id,
            Name = questionnaire.Name,
            Description = questionnaire.Description,
          
        };
    }

    public async Task<bool> AddQuestionnaire(QuestionnaireAddViewModel model,DateTime dueDate)
    {
        try
        {
            var questionnaire = new Questionnaire
            {
                Name = model.Name,
                Description = model.Description,
                DueDate = dueDate
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

    public async Task<bool>  AddSubmit(SubmitQuestionnaireAddViewModel model, string userId, Guid questionnaireId)
    {
        try
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
            var submitQuestionnaire = new SubmitQuestionnaire
            {
                
              Answer   = model.Answer,
            User = user,
              UserId = userId,
              QuestionnaireId = questionnaireId
            };

            var questionnaire = await this.repository.GetByIdAsync<Questionnaire>(questionnaireId);
            questionnaire.SubmitQuestionnaires.Add(submitQuestionnaire);
            await this.repository.AddAsync(submitQuestionnaire);
            await this.repository.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}