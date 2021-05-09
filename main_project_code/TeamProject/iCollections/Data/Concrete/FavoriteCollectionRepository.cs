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

        public FavoriteCollection CreateFavoriteCollectionByName(IcollectionUser user, string name)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User must not be null to Create");
            }
            if (name == null)
            {
                throw new ArgumentNullException("Name must not be null to Create");
            }

            var collection = new FavoriteCollection
            {
                Name = name,
                DateMade = DateTime.UtcNow,
                UserId = user.Id,
                Visibility = 1,
                Route = ""
                
            };

            if (collection != null)
            {
                AddOrUpdate(collection);
                return collection;
            }
            else
            {
                return null;
            }
            
        }
    }
}