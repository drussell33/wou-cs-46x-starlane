using System;
using System.Collections.Generic;
using System.Linq;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
            Path.GetFileNameWithoutExtension("sdsdf");
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
            var user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(i => i.AspnetIdentityId == complexId);
            return user.Id;
        }

        public static string GetICollectionUserName(string complexId, ICollectionsDbContext _collectionsDbContext)
        {
            var user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(i => i.AspnetIdentityId == complexId);
            return user.UserName;
        }

        public static bool IsBothFriendsOfMine(List<IcollectionUser> directs, IcollectionUser u1, IcollectionUser u2)
        {
            return directs.Any(r => r.Id == u1.Id) && directs.Any(r => r.Id == u2.Id);
        }

        // removes duplicates from list (list of mutual friends)
        // remove duplicate friendships 
        // (assuming all friendships have at least one of my friends in them)
        public static void RemoveDuplicates(ref List<FriendsWith> friendships, List<IcollectionUser> directFriends)
        {
            if (friendships == null || directFriends == null) throw new NullReferenceException("Cannot access null lists");
            
            var filtered = new List<FriendsWith>();
            var bothDirectFriends = new List<FriendsWith>();
            foreach (var ship in friendships)
            {
                if (directFriends.Any(r => r.Id == ship.User1.Id)
                    && !bothDirectFriends.Any(r => r.User1.Id == ship.User2.Id && r.User2.Id == ship.User1.Id))
                {
                    filtered.Add(ship);

                    if (IsBothFriendsOfMine(directFriends, ship.User1, ship.User2))
                    {
                        bothDirectFriends.Add(ship);
                    }
                }
            }
            friendships = filtered;
        }

        public void ReadDistantFriends(List<IcollectionUser> myFriends, List<FriendsWith> myFriendsFriends, List<Collection> myFriendCollections, int userId)
        {
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
            RemoveDuplicates(ref myFriendsFriends, myFriends);
        }

        public void ReadFollowees(List<IcollectionUser> followees, List<Follow> topFollow, List<Collection> followeesCollections, int userId)
        {
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

        public void OrderLists(List<FriendsWith> myFriendsFriends, List<Follow> topFollow, List<Collection> extractedCollections)
        {
            myFriendsFriends = myFriendsFriends.OrderByDescending(r => r.Began).ToList();
            topFollow = topFollow.OrderByDescending(r => r.Began).ToList();
            extractedCollections = extractedCollections.OrderByDescending(r => r.DateMade).ToList();
        }

        public List<PhotoInfo> GetMyPhotosInfo(int myId)
        {
            //string address = "https://localhost:44372/api/image/thumbnail/";
            string address = "https://icollections.azurewebsites.net/api/image/thumbnail/";
            var photosInformation = _collectionsDbContext.Photos
                                .Where(row => row.User.Id == myId)
                                .Select(myRows => new PhotoInfo { Url = address + myRows.PhotoGuid, PhotoName = myRows.Name })
                                .ToList();

            return photosInformation;
        }

        public Photo GetPhoto(Guid id)
        {
            var photo = _collectionsDbContext.Photos.FirstOrDefault(row => row.PhotoGuid == id);
            return photo;
        }

    }
}
