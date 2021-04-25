using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;


namespace iCollections.Data.Concrete
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public Collection GetIcollectionsByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}