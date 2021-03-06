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
            var j = GetAll();
            return GetAll()
                    .Include(f => f.User1)
                    .Include(f => f.User2)
                    .Where(row => row.User1.Id == userId || row.User2.Id == userId)
                    .ToList();
        }

        public List<IcollectionUser> GetMyFriends(int myId)
        {
            var myFriendsQuery = GetAll()
                .Include(f => f.User1)
                .Include(f => f.User2)
                .Where(friendship => friendship.User1.Id == myId || friendship.User2.Id == myId)
                .Select(friendship => friendship.User1.SelectOtherUser(friendship.User2, myId))
                .ToList();

            var myFriends = myFriendsQuery.GroupBy(f => f.Id).Select(f => f.FirstOrDefault()).ToList();
            return myFriends;
        }
    }
}
