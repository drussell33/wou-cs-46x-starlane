using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Models;

namespace iCollections.Data
{
    public class FollowService
    {
        private IDbContext _context;
        public FollowService(IDbContext context)
        {
            _context = context;
        }
        public Follow AddFollow(int follower, int followed)
        {
            var follow = new Follow { Follower = follower, Followed = followed };
            _context.Follows.Add(follow);
            _context.SaveChanges();
            return follow;
        }

        public List<Follow> GetFollows()
        {
            var query = _context.Follows.ToList();
            return query;
        }

        public async Task<List<Follow>> GetFollowsAsync()
        {
            var query = _context.Follows.ToList();
            return query;
        }
    }
}
