using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Models.Questionnaire;
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
            Assert.AreEqual(count.Count(), 2);
        }


        //GetQuestionnaireDetailsAsAdmin
        [Test]
        public void UnknownQuestionnaireMustThrowOnGetQuestionnaireDetailsAsAdmin()
        {

             var service = serviceProvider.GetService<IQuestionnaireService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetQuestionnaireDetailsAsAdmin(Guid.Empty), "Unknown questionnaire");
        }

        [Test]
        public async Task KnownQuestionnaireMustNotThrowOnGetQuestionnaireDetailsAsAdmin()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();

            
            Assert.DoesNotThrowAsync(async () => await service.GetQuestionnaireDetailsAsAdmin(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")));
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

            DateTime date;
            bool isTrue = DateTime.TryParseExact("23/04/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            Assert.IsFalse(await  service.AddQuestionnaire(questionnaire,date));
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
            DateTime date;
            bool isTrue = DateTime.TryParseExact("23/04/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            Assert.IsTrue(await service.AddQuestionnaire(questionnaire,date));
        }

        //GetAllQuestionnairiesForUser
        [Test]
        public async Task QuestionnaireMustThrowOnGetAllQuestionnairiesForUser()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();


            var questionnaire = new QuestionnaireAddViewModel();

          
            Assert.CatchAsync<ArgumentException>(async () => await service.GetAllQuestionnairiesForUser(""), "Unknown questionnaire");
        }

        [Test]
        public async Task QuestionnaireMustNotThrowOnGetAllQuestionnairiesForUser()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();


            var actual = await service.GetAllQuestionnairiesForUser("0f8fad5b-d9cb-469f-a165-70867728950a");
           
            Assert.DoesNotThrowAsync(async () => await service.GetAllQuestionnairiesForUser("0f8fad5b-d9cb-469f-a165-70867728950a"));
            Assert.AreEqual( 1, actual.Count());
        }


        //AddSubmit
        [Test]
        public async Task IsItCorrectOnAddSubmit()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();


            var submitQuestionnaire = new SubmitQuestionnaireAddViewModel()
            {
                Answer = ""
            };
            Assert.IsTrue(await service.AddSubmit(submitQuestionnaire, "0f8fad5b-d9cb-469f-a165-70867728950a", Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")));
        }

        [Test]
        public async Task IsItNotCorrectOnAddSubmit()
        {

            var service = serviceProvider.GetService<IQuestionnaireService>();

            var submitQuestionnaire = new SubmitQuestionnaireAddViewModel();

            Assert.IsFalse(await  service.AddSubmit(submitQuestionnaire,"", Guid.Empty));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            DateTime date;
             DateTime.TryParseExact("23/04/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            var questionnaire = new Questionnaire
            {
                Name = "test",
                Description = "test",
                DueDate = date
            }; 
            questionnaire.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

            var questionnaire2 = new Questionnaire
            {
                Name = "test",
                Description = "test",
                DueDate = date
            };
            questionnaire2.Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a");


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
            await repo.AddAsync(questionnaire2);

            await repo.SaveChangesAsync();
        }
    }
}