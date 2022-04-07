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
    public class ImageServiceTest
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
                .AddSingleton<IImageService, ImageService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }


        //DeleteItemImage
        [Test]
        public  async Task UnknownItemImageMustThrowOnDeleteItemImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteItemImage(Guid.Empty), "Unknown image");
        }

        public async Task KnownItemImageMustNotThrowOnDeleteItemImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteItemImage(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")));
        }

        //GetItemImage
        [Test]
        public void UnknownItemImageMustThrowOnGetItemImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetItemImage(Guid.Empty), "Unknown category");
        }

        [Test]
        public async Task KnownItemImageMustNotThrowOnGetItemImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            var actual = await service.GetItemImage(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"));
            var bytes = Array.Empty<byte>();
            Assert.DoesNotThrowAsync(async () => await service.GetItemImage(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b")));
            Assert.AreEqual(bytes,actual );
        }

        //GetCategoryImage
        [Test]
        public async Task UnknownCategoryImageMustThrowOnGetCategoryImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            
            Assert.ThrowsAsync<ArgumentException>(async () => await service.GetCategoryImage(Guid.Empty));

        }

        [Test]
        public async Task KnownCategoryImageMustNotThrowOnGetCategoryImage()
        {

            var service = serviceProvider.GetService<IImageService>();

            var actual = await service.GetItemImage(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"));
            var bytes = Array.Empty<byte>();
            Assert.DoesNotThrowAsync(async ()=>await service.GetCategoryImage(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")));
            Assert.AreEqual(bytes, actual);
        }

        

        //AddImageProfile
        [Test]
        public async Task KnownUserMustNotThrowOnAddImageProfile()
        {

            var service = serviceProvider.GetService<IImageService>();
           

            var image = new Image
            {
                Source = Array.Empty<byte>()
            };

            Assert.DoesNotThrowAsync(async () => await service.AddImageProfile(image, "0f8fad5b-d9cb-469f-a165-70867728950d"));
        }

        [Test]
        public async Task UnknownUserMustThrowOnAddImageProfile()
        {

            var service = serviceProvider.GetService<IImageService>();
           

            var image = new Image
            {
                Source = Array.Empty<byte>()
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await service.AddImageProfile(image, "0f8fad5b-d9cb-469f-a165-70867728950o"), "Unknow user");
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
                Phone = "+359 123 4567"
            };
            user.Id = "0f8fad5b-d9cb-469f-a165-70867728950d";
            var category = new Category
                {
                    Name = "Car",
                    ImageId = image.Id
                };

          
          
            category.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a");
            var item = new Item()
            {
                Label = "car",
                Description = "",
                Price = 0,
                CategoryId = category.Id,
                OwnerId = user.Id
            };

            user.MyItems.Add(item);
            category.Items.Add(item);

            item.Owner = user;
            item.Category = category;

            var images = new List<ItemImages>();

            var itemImage = new ItemImages()

            {
                ItemId = item.Id,
                ImageId = image.Id
            };
            itemImage.Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b");
            images.Add(itemImage);
            item.Images = images;

            await repo.AddAsync(category);
           await repo.AddAsync(user);
           await repo.AddAsync(image);
               await repo.AddAsync(itemImage);

               await repo.SaveChangesAsync();
        }
    }
}