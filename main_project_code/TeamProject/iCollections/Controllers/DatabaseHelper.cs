using System;
using System.Collections.Generic;
using System.Linq;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iCollections.Controllers
{
    // This class solely reads from the database
    public class DatabaseHelper
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public DatabaseHelper(UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        public bool isKeyInFriendship(IcollectionUser user1, IcollectionUser user2, int key)
        {
            return key == user1.Id || key == user2.Id;
        }

        public List<IcollectionUser> GetMyFriends(int myId)
        {
            var myFriendsQuery = _collectionsDbContext.FriendsWiths
                .Include(f => f.User1)
                .Include(f => f.User2)
                .Where(friendship => friendship.User1.Id == myId || friendship.User2.Id == myId)
                .Select(friendship => friendship.User1.SelectOtherUser(friendship.User2, myId))
                .ToList();

            var myFriends = myFriendsQuery.GroupBy(f => f.Id).Select(f => f.FirstOrDefault()).ToList();
            return myFriends;
        }

        public List<IcollectionUser> GetMyFollowees(int myId)
        {
            var whoIFollow = _collectionsDbContext.Follows
                .Where(f => f.FollowerNavigation.Id == myId)
                .Select(f => f.FollowedNavigation)
                .ToList();

            return whoIFollow;
        }

        public static int GetReadableUserID(string complexId, ICollectionsDbContext _collectionsDbContext)
        {
            var user = _collectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == complexId);
            return user.Id;
        }

        private void RemoveDuplicates(List<FriendsWith> list, List<IcollectionUser> directFriends)
        {
            for (int i = list.Count() - 1; i >= 0; i--)
            {
                var user1 = list[i].User1.Id;
                var user2 = list[i].User2.Id;
                if (directFriends.Any(myBuddy => myBuddy.Id == user2)) list.RemoveAll(r => r.User1.Id == user1 && r.User2.Id == user2);
                else list.RemoveAll(r => r.User1.Id == user2 && r.User2.Id == user1);
            }
        }

        public void ReadDistantFriends(List<IcollectionUser> myFriends, List<FriendsWith> myFriendsFriends, List<Collection> myFriendCollections, int userId) {
            foreach (var directFriend in myFriends)
            {
                var directFriendsFriend = _collectionsDbContext.FriendsWiths
                    .Include(f => f.User1)
                    .Include(f => f.User2)
                    .Where(row => row.User1.Id == directFriend.Id || row.User2.Id == directFriend.Id)
                    .ToList();

                var myBuddyCollections = _collectionsDbContext
                    .Collections
                    .Include(r => r.User)
                    .Where(r => r.User.Id == directFriend.Id)
                    .ToList();

                myFriendCollections.AddRange(myBuddyCollections);
                myFriendsFriends.AddRange(directFriendsFriend);
            }

            myFriendsFriends.RemoveAll(friendship => isKeyInFriendship(friendship.User1, friendship.User2, userId));
            RemoveDuplicates(myFriendsFriends, myFriends);
        }

        public void ReadFollowees(List<IcollectionUser> followees, List<Follow> topFollow, List<Collection> followeesCollections, int userId) {
            foreach (var myFollowee in followees)
            {
                var followeeFollowees = _collectionsDbContext.Follows
                    .Include(f => f.FollowedNavigation)
                    .Include(f => f.FollowerNavigation)
                    .Where(row => row.FollowerNavigation.Id == myFollowee.Id && row.FollowedNavigation.Id != userId)
                    .ToList();

                var myFolloweeCollections = _collectionsDbContext.Collections
                    .Include(r => r.User)
                    .Where(c => c.User.Id == myFollowee.Id)
                    .ToList();

                topFollow.AddRange(followeeFollowees);
                followeesCollections.AddRange(myFolloweeCollections);
            }
        }

        public void OrderLists(List<FriendsWith> myFriendsFriends, List<Follow> topFollow, List<Collection> extractedCollections) {
            myFriendsFriends = myFriendsFriends.OrderByDescending(r => r.Began).ToList();
            topFollow = topFollow.OrderByDescending(r => r.Began).ToList();
            extractedCollections = extractedCollections.OrderByDescending(r => r.DateMade).ToList();
        }

    }
}
