using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace iCollections.Data.Concrete
{
    public class FavoriteCollectionRepository : Repository<FavoriteCollection>, IFavoriteCollectionRepository
    {
        public FavoriteCollectionRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }
        public List<FavoriteCollection> GetMyFavoritesByUser(IcollectionUser user)
        {
            return _dbSet.Include(c => c.Collect).Where(u => u.User == user && u.Name == "My Favorites").ToList();
        }

        public void DeleteByCollectionId(int id)
        {
            FavoriteCollection favorite = _dbSet.Where(s => s.CollectId == id).FirstOrDefault();
            _dbSet.Remove(favorite);
            _context.SaveChanges();

        }

    }
}