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
    public class FriendsWithRepository : Repository<FriendsWith>, IFriendsWithRepository
    {
        public FriendsWithRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<FriendsWith> GetFriendshipsInvolvingThisUser(int userId)
        {
            return GetAll()
                    .Include(f => f.User1)
                    .Include(f => f.User2)
                    .Where(row => row.User1.Id == userId || row.User2.Id == userId)
                    .ToList();
        }
    }
}
