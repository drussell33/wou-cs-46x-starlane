using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iCollections.Models;
using iCollections.Data.Abstract;
using Microsoft.EntityFrameworkCore.Query;

namespace iCollections.Data.Concrete
{
    public class FollowRepository : Repository<Follow>, IFollowRepository
    {
        public FollowRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public virtual bool Exists(Follow follow)
        {
            return _dbSet.Any(f => f == follow);
        }

        public virtual bool Exists(int id)
        {
            return _dbSet.Any(f => f.Id == id);
        }

        public virtual IIncludableQueryable<Follow, IcollectionUser> GetFollows()
        {
            var follows = _dbSet.Include(f => f.FollowedNavigation).Include(f => f.FollowerNavigation);
            return follows;
        }

        public virtual Follow GetFollow(Func<Follow, bool> filter)
        {
            var follow = _dbSet.Include(f => f.FollowedNavigation).Include(f => f.FollowerNavigation).FirstOrDefault(x => filter(x));
            return follow;
        }

        public virtual Follow GetFollowLight(Func<Follow, bool> filter)
        {
            var follow = _dbSet.FirstOrDefault(x => filter(x));
            return follow;
        }

        public virtual async Task<Follow> GetFollowAsync(Func<Follow, bool> filter)
        {
            var follow = await _dbSet.Include(f => f.FollowedNavigation).Include(f => f.FollowerNavigation).FirstOrDefaultAsync(x => filter(x));
            return follow;
        }

    }
}
