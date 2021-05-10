using iCollections.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace iCollections.Data.Abstract
{
    public interface ICollectionPhotoRepository : IRepository<CollectionPhoto>
    {
        List<CollectionPhoto> GetAllCollectionPhotosbyCollectionId(int collectionId);
    }
}