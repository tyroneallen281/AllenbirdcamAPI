using ABC.Data.DatabaseContext;
using ABC.Domain.Entities;
using ABC.Repository.Common;

namespace ABC.Repository.Repos
{
    public class SightingRepository : RepositoryBase<Sighting, ABCDbContext>, ISightingRepository
    {
        public SightingRepository(ABCDbContext dbContext) : base(dbContext)
        {
        }
    }
}