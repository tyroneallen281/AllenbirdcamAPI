using ABC.Data.DatabaseContext;
using ABC.Domain.Entities;
using ABC.Repository.Common;

namespace ABC.Repository.Repos
{
    public class VoteRepository : RepositoryBase<Vote, ABCDbContext>, IVoteRepository
    {
        public VoteRepository(ABCDbContext dbContext) : base(dbContext)
        {
        }
    }
}