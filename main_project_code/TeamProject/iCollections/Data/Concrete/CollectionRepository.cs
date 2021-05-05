using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;


namespace iCollections.Data.Concrete
{
    public class CollectionRepository : Repository<Collection>, IcollectionRepository
    {
        public CollectionRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<Collection> GetMostRecentiCollections(int userId, int howMany)
        {
            return GetAll().Where(c => c.User.Id == userId).OrderByDescending(c => c.DateMade).Take(howMany).ToList();
        }

        public Collection GetCollectionById(int? collectionID)
        {
            return GetAll().Where(m => m.Id == collectionID).FirstOrDefault();
        }


    }
}