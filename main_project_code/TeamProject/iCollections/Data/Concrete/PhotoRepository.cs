using iCollections.Models;
using iCollections.Data.Abstract;

namespace iCollections.Data.Concrete
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ICollectionsDbContext ctx) : base(ctx)
        {
            
        }
    }
}