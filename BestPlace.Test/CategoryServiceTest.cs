using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BestPlace.Core.Contracts;
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
    public class CategoryServiceTest
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
                .AddSingleton<ICategoryService, CategoryService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }


        //GetAll
        [Test]
        public  async  Task GetAllCategories()
        {

            var service = serviceProvider.GetService<ICategoryService>();

            var actual =  await service.All();

           Assert.AreEqual(1, actual.Count() );
        }


        //GetCategoryForEdit
        [Test]
        public void UnknownCategoryMustThrowOnEdit()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            var physicalFile = new FileInfo("img.jpg");
            IFormFile formFile = physicalFile.AsMockIFormFile();
            var category = new CategoryEditViewModel()
            {
              Id  = Guid.Empty,
              Image = formFile,
              Name = "Car12"
            };

            Assert.CatchAsync<ArgumentException>(async () => await service.EditCategory(category), "Unknown category");
        }

        [Test]
        public void KnownCategoryMustNotThrowOnEdit()
        {

            var service = serviceProvider.GetService<ICategoryService>();

            var physicalFile = new FileInfo("img.jpg");
            IFormFile formFile = physicalFile.AsMockIFormFile();
            var category = new CategoryEditViewModel()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Image = formFile,
                Name = "Car12"
            };

            Assert.DoesNotThrowAsync(async () => await service.EditCategory(category));
        }

        //AddCategory
        [Test]
        public async Task MustReturnFalseOnAddCategory()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            
            var category = new CategoryAddViewModel()
            {
                Name = null,
               Image = null
            };

            Assert.IsFalse(  await service.AddCategory(category));
        }

        [Test]
        public async Task MustNotReturnFalseOnAddCategory()
        {

            var service = serviceProvider.GetService<ICategoryService>();

            var physicalFile = new FileInfo("img.jpg");
            IFormFile formFile = physicalFile.AsMockIFormFile();

            var category = new CategoryAddViewModel()
            {
                Name = "sd",
                Image = formFile
            };

            Assert.IsTrue(await service.AddCategory(category));
        }

        //DeleteCategory
        [Test]
        public void UnknownCategoryMustThrowOnDeleteCategory()
        {

            var service = serviceProvider.GetService<ICategoryService>();


            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteCategory(Guid.Empty), "Unknown category");
        }

        [Test]
        public void KnownCategoryMustNotThrowOnDeleteCategory()
        {

            var service = serviceProvider.GetService<ICategoryService>();


            Assert.DoesNotThrowAsync(async () => await service.DeleteCategory(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")));
        }


        //GetCategoryForEdit
        [Test]
        public void UnknownCategoryMustThrowOnGetCategoryForEdit()
        {
            
            var service = serviceProvider.GetService<ICategoryService>();


            Assert.CatchAsync<ArgumentException>(async () => await service.GetCategoryForEdit(Guid.Empty), "Unknown category");
        }

        [Test]
        public void KnownCategoryMustNotThrowOnGetCategoryForEdit()
        {

            var service = serviceProvider.GetService<ICategoryService>();


            Assert.DoesNotThrowAsync(async () => await service.GetCategoryForEdit(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var image = new Image
            {
                Source = Array.Empty<byte>()
            };
           
            var user = new ApplicationUser()
            {
                Address = "",
                Email = "test@gmail.com",
                FirstName = "",
                LastName = "",
                Phone = "+359 123 4567",
            };
            
          
            var category = new Category
                {
                    Name = "Car",
                    ImageId = image.Id
                };
           
            category.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            var item = new Item()
            {
                Label = "car",
                Description = "",
                Price = 0,
                CategoryId = category.Id,
                OwnerId = user.Id
            };
            var itemImage = new ItemImages()

            {
                ItemId = item.Id,
                ImageId = image.Id
            };
            await repo.AddAsync(image);
            await repo.AddAsync(item);
            await repo.AddAsync(itemImage);
            await repo.AddAsync(user);
            await repo.AddAsync(category);
            await repo.SaveChangesAsync();
        }
    }
}