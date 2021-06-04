using iCollections.Models;
using System.Collections.Generic;
using System;

namespace iCollections.Data.Abstract
{
    public interface IFavoriteCollectionRepository : IRepository<FavoriteCollection>
    {
        List<FavoriteCollection> GetMyFavoritesByUser(IcollectionUser user);

        void DeleteByCollectionId(int id);

    }
}
