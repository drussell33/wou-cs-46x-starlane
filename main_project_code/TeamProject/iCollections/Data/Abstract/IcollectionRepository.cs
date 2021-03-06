using iCollections.Models;
using System.Collections.Generic;
using System;

namespace iCollections.Data.Abstract
{
    public interface IcollectionRepository : IRepository<Collection>
    {
        List<Collection> GetMostRecentiCollections(int userId, int howMany);

        List<Collection> GetICollectionsForThisUser(int userId);

        Collection GetCollectionById(int collectionID);
    }
}