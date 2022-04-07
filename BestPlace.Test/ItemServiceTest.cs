using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Category;
using BestPlace.Core.Models.Item;
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
    public class ItemServiceTest
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
                .AddSingleton<IItemService, ItemService>()
                .BuildServiceProvider();
            ;

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo );
        }


        //GetCount
        [Test] 
        public async Task GetCount()
        {

            var service = serviceProvider.GetService<IItemService>();
            var actual = await service.All();


            Assert.AreEqual( 2, actual.Count());

        }

        //AllPublic
        [Test]
        public async Task AllPublicWithQuery()
        {

            var service = serviceProvider.GetService<IItemService>();
            var actual = await service.AllPublic(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"),"2");


            Assert.AreEqual(1, actual.Items.Count());
            Assert.AreEqual("2", actual.Items.First().Label);

        }

        [Test]
        public async Task AllPublicWithOutQuery()
        {

            var service = serviceProvider.GetService<IItemService>();
            var actual = await service.AllPublic(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"), null);


            Assert.AreEqual(2, actual.Items.Count());
        }


        //DeleteItem
        [Test]
        public async Task UnkownItemMustThrowOnDeleteItem()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteItem(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950d"), "0f8fad5b-d9cb-469f-a165-70867728950d"), "Unknown item");
        }
        [Test]
        public async Task KnowItemMustNotThrowOnDeleteItem()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.DoesNotThrowAsync(async () => await service.DeleteItem(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"), "0f8fad5b-d9cb-469f-a165-70867728950d"));
        }

        //GetItemForEdit
        [Test]
        public async Task UnkownItemMustThrowOnGetItemForEdit()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.GetItemForEdit(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950d"), "0f8fad5b-d9cb-469f-a165-70867728950d"), "Unknown item");
        }
        [Test]
        public async Task KnowItemMustNotThrowOnGetItemForEdit()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.DoesNotThrowAsync(async () => await service.GetItemForEdit(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"), "0f8fad5b-d9cb-469f-a165-70867728950d"), "Unknown item");
        }


        //AddItem
        [Test]
        public async Task AddItemMustReturnTrueOnAddItem()
        {
            var service = serviceProvider.GetService<IItemService>();
            var item = new ItemAddViewModel
            {
                CategoryId = "0f8fad5b-d9cb-469f-a165-70867728950b",
                Description = "",
                Label = "",
                Price = 15
            };
            var physicalFile = new FileInfo("img.jpg");
            IFormFile formFile = physicalFile.AsMockIFormFile();
            item.Images.Add(formFile);
            Assert.IsTrue(await service.AddItem(item,"0f8fad5b-d9cb-469f-a165-70867728950d"));

        }
        [Test]
        public async Task AddItemMustNotReturnTrueOnAddItem()
        {
            var service = serviceProvider.GetService<IItemService>();
            var item = new ItemAddViewModel();
            Assert.IsFalse(await service.AddItem(item, "0f8fad5b-d9cb-469f-a165-70867728950d"));
        }

        //IsMine
        [Test]
        public async Task NotOwnItemMustThrowOnIsMine()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.IsMine(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"), "0f8fad5b-d9cb-469f-a165-70867728950f"), "This is not your item");
        }
        public async Task UnknowUserMustThrowOnIsMine()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.IsMine(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950r"), "0f8fad5b-d9cb-469f-a165-70867728950f"), "Unknow user");
        }
        public async Task KnowUserAndOwnItemMustNotThrowOnIsMine()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.DoesNotThrowAsync(async () => await service.IsMine(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"), "0f8fad5b-d9cb-469f-a165-70867728950d"));
        }
        //GetItemDetails
        [Test]
        public async Task UnkownItemMustThrowOnGetItemDetails()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.GetItemDetails(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950d")), "Unknown item");
        }
        [Test]
        public async Task KnowItemMustThrowOnGetItemDetails()
        {
            var service = serviceProvider.GetService<IItemService>();
            Assert.DoesNotThrowAsync(async () => await service.GetItemDetails(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var user1 = new ApplicationUser()
            {
                Address = "fsde",
                Email = "test@gmail.com",
                FirstName = "df",
                LastName = "fsdf",
                Phone = "+359 123 4567"
            };
            user1.Id = "0f8fad5b-d9cb-469f-a165-70867728950d";

            var user2 = new ApplicationUser()
            {
                Address = "fsde",
                Email = "test@gmail.com",
                FirstName = "df",
                LastName = "fsdf",
                Phone = "+359 123 4567"
            };
            user2.Id = "0f8fad5b-d9cb-469f-a165-70867728950f";

            var image = new Image
            {
                Source = Array.Empty<byte>()
            };

            var category = new Category
            {
                Name = "Car",
                ImageId = image.Id
            };
            category.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b");
            var item1 = new Item
            {
                Label = "1",
                CategoryId = category.Id,
                Description = "",
                OwnerId = user1.Id,
                Price = 0,
            };

            var item2 = new Item
            {
                Label = "2",
                CategoryId = category.Id,
                Description = "",
                OwnerId = user1.Id,
                Price = 0,
            };
            item2.Id =Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a");
            var itemImage1 = new ItemImages()
            {
                ImageId = image.Id,
                ItemId = item1.Id
            };
            item1.Images.Add(itemImage1);

            var itemImage2 = new ItemImages()
            {
                ImageId = image.Id,
                ItemId = item2.Id
            };
            item2.Images.Add(itemImage2);

           user1.MyItems.Add(item2);
           user1.MyItems.Add(item1);

            await repo.AddAsync(user1);
            await repo.AddAsync(user2);
            await repo.AddAsync(category);
            await repo.AddAsync(image);
            await repo.AddAsync(item1);
            await repo.AddAsync(itemImage1);
            await repo.AddAsync(item2);
            await repo.AddAsync(itemImage2);
            await repo.SaveChangesAsync();
        }
    }
}