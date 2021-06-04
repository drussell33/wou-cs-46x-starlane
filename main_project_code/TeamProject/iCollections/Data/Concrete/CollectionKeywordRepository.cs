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

        public List<CollectionKeyword> GetPublicCollectionKeywordsByUser(IcollectionUser user)
        {
            return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).ThenInclude(cp=>cp.CollectionPhotoes).ThenInclude(p=>p.Photo).Where(c => c.Collect.User == user && c.Collect.Visibility == 1).ToList();
        }

        public List<CollectionKeyword> GetPublicAndPrivateCollectionKeywordsByUser(IcollectionUser user)
        {
            return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).ThenInclude(cp => cp.CollectionPhotoes).ThenInclude(p => p.Photo).Where(c => c.Collect.User == user).ToList();
        }

        public List<CollectionKeyword> GetPublicCollectionKeywordsByUserSortedAscending(IcollectionUser user, string sort)
        {
            if (sort == "name")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user && c.Collect.Visibility == 1).OrderBy(n => n.Collect.Name).ToList();
            }

            else if (sort == "keyword")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user && c.Collect.Visibility == 1).OrderBy(n => n.Keyword.Name).ToList();
            }

            else if (sort == "date")
            {
                return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user && c.Collect.Visibility == 1).OrderBy(n => n.Collect.DateMade).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CollectionKeyword> GetUserPublicCollectionKeywordsByKeyword(IcollectionUser user, string keyword)
        {
            return _dbSet.Include(c => c.Collect).Include(k => k.Keyword).Where(c => c.Collect.User == user && c.Collect.Visibility == 1 && c.Keyword.Name.Contains(keyword)).OrderBy(c => c.Collect.Name).ToList();
        }

        public List<CollectionKeyword> GetMyFavoriteCollectionsByUser(IcollectionUser user)
        {
            return _dbSet.Include(ck => ck.Keyword).Include(c => c.Collect).ThenInclude(u => u.User).ThenInclude(u => u.FavoriteCollections).Where(c => c.Collect.FavoriteCollections.FirstOrDefault().User == user && c.Collect.Visibility == 1).ToList();
        }
    }
}