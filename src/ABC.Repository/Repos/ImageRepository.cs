using ABC.Data.DatabaseContext;
using ABC.Domain.Entities;
using ABC.Repository.Common;

namespace ABC.Repository.Repos
{
    public class ImageRepository : RepositoryBase<Image, ABCDbContext>, IImageRepository
    {
        public ImageRepository(ABCDbContext dbContext) : base(dbContext)
        {
        }
    }
}