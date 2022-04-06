using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Statistics;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;

namespace BestPlace.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IApplicatioDbRepository repository;


        public StatisticsService(IApplicatioDbRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AllCounts> GetCounts()
        {
           int userCount =  this.repository.All<ApplicationUser>().Count();
            int itemsCount = this.repository.All<Item>().Count();

            return new AllCounts
            {
                UsersCount = userCount,
                ItemsCount = itemsCount,
            };
        }
    }
}
