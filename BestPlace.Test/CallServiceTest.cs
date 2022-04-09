using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BestPlace.Core.Constants;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Call;
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
    public class CallServiceTest
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
                .AddSingleton<ICallService, CallService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }


        //DeleteItemImage
        [Test]
        public  async Task UnknownCallMustThrowOnDeleteCall()
        {

            var service = serviceProvider.GetService<ICallService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.DeleteCall(Guid.Empty), "Unknown call");
        }

        [Test]
        public async Task KnownCallMustNotThrowOnDeleteCall()
        {

            var service = serviceProvider.GetService<ICallService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteCall(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")));
        }

        //DeleteItemImage
        [Test]
        public async Task UnValidCallMustReturnFalseOnAddCall()
        {

            var service = serviceProvider.GetService<ICallService>();
            var call = new CallAddViewModel();
            Assert.IsFalse(await service.AddCall(call, ""));
        }

        [Test]
        public async Task ValidCallMustReturnTrueOnAddCall()
        {

            var service = serviceProvider.GetService<ICallService>();
            
            var call = new CallAddViewModel()
            {
                Problem = "",
            };
            Assert.IsTrue( await service.AddCall(call, "0f8fad5b-d9cb-469f-a165-70867728950d"));
        }


        //GetAllCalls
        [Test]
        public  async Task GetAllCalls()
        {

            var service = serviceProvider.GetService<ICallService>();
            var actual = await service.All();
            Assert.AreEqual(1 , actual.Count());
        }

       

        //DetailsCall
        [Test]
        public async Task UnknowCallMustThrowOnGetCallDetails()
        {

            var service = serviceProvider.GetService<ICallService>();

            
            Assert.ThrowsAsync<ArgumentException>(async () => await service.GetCallDetails(Guid.Empty));

        }

        [Test]
        public async Task KnowCallMustNotThrowOnGetCallDetails()
        {

            var service = serviceProvider.GetService<ICallService>();

            Assert.DoesNotThrowAsync(async ()=>await service.GetCallDetails(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")));
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
                Address = "",
                Email = "test@gmail.com",
                FirstName = "",
                LastName = "",
                Phone = "+359 123 4567",
            };
            user.Id = "0f8fad5b-d9cb-469f-a165-70867728950d";
            var call = new Call()
                {
                    Problem = "",
                    UserId = user.Id
                };

            call.Id= Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"); 
            await repo.AddAsync(user);
            await repo.AddAsync(call);

               await repo.SaveChangesAsync();
        }
    }
}