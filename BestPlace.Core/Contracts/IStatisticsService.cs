using BestPlace.Core.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPlace.Core.Contracts
{
     public interface IStatisticsService
    {
        Task<AllCounts> GetCounts();
    }
}
