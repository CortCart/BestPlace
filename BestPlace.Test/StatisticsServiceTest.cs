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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BestPlace.Test
{
    public class StatisticsServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();
           // RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>();
            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IStatisticsService, StatisticsService>()
                .BuildServiceProvider();
            ;

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo );
        }


        //GetCounts
        [Test] 
        public async Task GetCounts()
        {

            var service = serviceProvider.GetService<IStatisticsService>();
            var counts =  await service.GetCounts();
            Assert.AreEqual(counts.UsersCount, 1);
            Assert.AreEqual(counts.CategoriesCount, 1);
            Assert.AreEqual(counts.ItemsCount, 1);
            Assert.AreEqual(counts.QuestionnaireCount, 1);

        }



        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var user = new ApplicationUser()
            {
                Address = "fsde",
                Email = "test@gmail.com",
                FirstName = "df",
                LastName = "fsdf",
                Phone = "+359 123 4567"
            };
            var image = new Image
            {
                Source = Array.Empty<byte>()
            };

            var category = new Category
            {
                Name = "Car",
                ImageId = image.Id
            };

            var item = new Item
            {
                Label = "",
                CategoryId = category.Id,
                Description = "",
                OwnerId = user.Id,
                Price = 0,
            };
            var itemImage = new ItemImages()
            {
                ImageId = image.Id,
                ItemId = item.Id
            };
            item.Images.Add(itemImage);

            var questionnaire = new Questionnaire
            {
                Name = "test",
                Description = "test"
            };

            await repo.AddAsync(user);
            await repo.AddAsync(category);
            await repo.AddAsync(image);
            await repo.AddAsync(item);
            await repo.AddAsync(itemImage);
            await repo.AddAsync(questionnaire);

            await repo.SaveChangesAsync();
        }
    }
}