using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace iCollections.Data.Concrete
{
    public class CollectionKeywordRepository : Repository<CollectionKeyword>, ICollectionKeywordRepository
    {
        public CollectionKeywordRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<CollectionKeyword> GetCollectionKeywordsByUser(IcollectionUser user)
        {
            return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).ToList();
        }

        public List<CollectionKeyword> GetCollectionKeywordsByUserSortedAscending(IcollectionUser user, string sort)
        {
            if (sort == "name")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).OrderBy(n => n.Collect.Name).ToList();
            }

            else if (sort == "keyword")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).OrderBy(n => n.Keyword.Name).ToList();
            }

            else if (sort == "date")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).OrderBy(n => n.Collect.DateMade).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CollectionKeyword> GetUserCollectionKeywordsByKeyword(IcollectionUser user, string keyword)
        {
            return _dbSet.Include(c => c.Collect).Include(k => k.Keyword).Where(c => c.Collect.User == user && c.Keyword.Name.Contains(keyword)).OrderBy(c => c.Collect.Name).ToList();
        }
    }
}