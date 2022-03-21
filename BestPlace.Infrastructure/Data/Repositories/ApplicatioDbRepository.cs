using BestPlace.Infrastructure.Data.Common;

namespace BestPlace.Infrastructure.Data.Repositories;

public class ApplicatioDbRepository : Repository, IApplicatioDbRepository
{
    public ApplicatioDbRepository(ApplicationDbContext context)
    {
        this.Context = context;
    }
}