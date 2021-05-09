using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;


namespace iCollections.Data.Concrete
{
    public class FavoriteCollectionRepository : Repository<FavoriteCollection>, IFavoriteCollectionRepository
    {
        public FavoriteCollectionRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public FavoriteCollection GetMyFavoritesByUser(IcollectionUser user)
        {
            return _dbSet.FirstOrDefault(u => u.User == user && u.Name == "My Favorites");
        }
    }
}