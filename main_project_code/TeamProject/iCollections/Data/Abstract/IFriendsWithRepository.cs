using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace iCollections.Data.Abstract
{
    public interface IFriendsWithRepository : IRepository<FriendsWith>
    {
        List<FriendsWith> GetFriendshipsInvolvingThisUser(int userId);

        List<IcollectionUser> GetMyFriends(int myId);
    }
}
