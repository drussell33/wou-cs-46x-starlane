using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace iCollections.Data.Concrete
{
    public class CollectionRepository : Repository<Collection>, IcollectionRepository
    {
        public CollectionRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<Collection> GetMostRecentiCollections(int userId, int howMany)
        {
            return GetAll().Where(c => c.User.Id == userId && c.Visibility == 1).OrderByDescending(c => c.DateMade).Take(howMany).ToList();
        }

        public List<Collection> GetICollectionsForThisUser(int userId)
        {
            return GetAll().Include(r => r.User).Where(r => r.User.Id == userId && r.Visibility == 1).ToList();
        }

        public Collection GetCollectionById(int collectionID)
        {
            return GetAll().Where(m => m.Id == collectionID).FirstOrDefault();
        }

        public List<Collection> GetMostRecentCompleteiCollections(int userId, int howMany)
        {
            //return GetAll().Where(c => c.User.Id == userId && c.Visibility == 1).OrderByDescending(c => c.DateMade).Take(howMany).ToList();
            //var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == appUser.UserName).Collections.OrderByDescending(c => c.DateMade);
            return GetAll().Where(c => c.User.Id == userId && c.Visibility == 1).Include("CollectionKeywords").Include("Keyword").OrderByDescending(c => c.DateMade).Take(howMany).ToList();
        }


    }
}