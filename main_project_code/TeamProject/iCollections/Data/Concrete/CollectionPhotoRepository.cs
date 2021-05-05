using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace iCollections.Data.Concrete
{
    public class CollectionPhotoRepository : Repository<CollectionPhoto>, ICollectionPhotoRepository
    {
        public CollectionPhotoRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<CollectionPhoto> GetAllCollectionPhotosbyCollectionId(int collectionId)
        {
            return GetAll().Where(c => c.CollectId == collectionId).ToList();
        }


    }
}