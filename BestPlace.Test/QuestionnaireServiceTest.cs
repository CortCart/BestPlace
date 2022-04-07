using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Services;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using BestPlace.Test.FileSimulation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BestPlace.Test
{
    public class QuestionnaireServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IQuestionnaireService, Core.Services.QuestionnaireService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }


        //AllQuestionnairies
        [Test] 
        public async Task GetAll ()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();
            var count =  await service.GetAllQuestionnairies();
            Assert.AreEqual(count.Count(), 1);
        }


        //GetQuestionnaireDetails
        [Test]
        public void UnknownQuestionnaireMustThrowOnGetQuestionnaireDetails()
        {

             var service = serviceProvider.GetService<IQuestionnaireService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetQuestionnaireDetails(Guid.Empty), "Unknown questionnaire");
        }

        [Test]
        public async Task KnownQuestionnaireMustNotThrowOnGetQuestionnaireDetails()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();

            
            Assert.DoesNotThrowAsync(async () => await service.GetQuestionnaireDetails(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")));
        }

        //AddQuestionnaire
        [Test]
        public async Task QuestionnaireMustReturnFalseOnAddQuestionnaire()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();
           
            
            var questionnaire = new QuestionnaireAddViewModel();
            Assert.IsFalse(await  service.AddQuestionnaire(questionnaire));
        }

        [Test]
        public async Task QuestionnaireMustReturnTrueOnAddQuestionnaire()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();


            var questionnaire = new QuestionnaireAddViewModel()
            {
                Name = "test",
                Description = "test"
            };
            Assert.IsTrue(await service.AddQuestionnaire(questionnaire));
        }

       

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var questionnaire = new Questionnaire
            {
                Name = "test",
                Description = "test"
            }; 
            questionnaire.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

            var user = new ApplicationUser()
            {
                Address = "fsde",
                Email = "test@gmail.com",
                FirstName = "df",
                LastName = "fsdf",
                Phone = "+359 123 4567"
            };

            user.Id = "0f8fad5b-d9cb-469f-a165-70867728950a";


            var submitQuestionnaire = new SubmitQuestionnaire()
            {
                Answer = "",
                QuestionnaireId = questionnaire.Id,
                UserId = user.Id
            };
            submitQuestionnaire.Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b");
            questionnaire.SubmitQuestionnaires.Add(submitQuestionnaire);
            await repo.AddAsync(questionnaire);
            await repo.AddAsync(user);
            await repo.AddAsync(submitQuestionnaire);

            await repo.SaveChangesAsync();
        }
    }
}