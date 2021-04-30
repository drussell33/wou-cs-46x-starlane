using System;
using System.Collections.Generic;
using System.Linq;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using iCollections.Data.Abstract;

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

        public static bool isKeyInFriendship(IcollectionUser user1, IcollectionUser user2, int key)
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
        // want ships in this format (directFriend, secondHandFriend)
        public static void RemoveDuplicates(ref List<FriendsWith> friendships, List<IcollectionUser> directFriends)
        {
            if (friendships == null || directFriends == null) throw new NullReferenceException("Cannot access null lists");
            
            var filtered = new List<FriendsWith>();
            foreach (var ship in friendships)
            {
                if (!directFriends.Any(df => df.Id == ship.User1.Id)) // if user1 != df -> skip
                {
                    continue;
                }
                else
                {
                    // if (df, other) actually already in filtered as (other, df) -> skip
                    if (filtered.Any(existing => existing.User1.Id == ship.User2.Id && existing.User2.Id == ship.User1.Id))
                    {
                        continue;
                    }
                    // if (df, other) not in filtered -> add
                    else if (!filtered.Any(existing => existing.User1.Id == ship.User1.Id && existing.User2.Id == ship.User2.Id))
                    {
                        filtered.Add(ship);
                    }
                }
            }
            friendships = filtered;
        }

        public static void ReadDistantFriends(List<IcollectionUser> myFriends, ref List<FriendsWith> myFriendsFriends, List<Collection> myFriendCollections, int userId, IFriendsWithRepository friends, IcollectionRepository collections)
        {
            // iterate through all my direct friends
            foreach (var directFriend in myFriends)
            {
                // add friendships with my direct friend in it
                var directFriendsFriend = friends.GetFriendshipsInvolvingThisUser(directFriend.Id);

                // add my direct friend's collections
                var myBuddyCollections = collections.GetICollectionsForThisUser(directFriend.Id);

                // append to my lists
                myFriendCollections.AddRange(myBuddyCollections);
                myFriendsFriends.AddRange(directFriendsFriend);
            }

            // take out friendships involving me personally
            myFriendsFriends.RemoveAll(friendship => isKeyInFriendship(friendship.User1, friendship.User2, userId));
            RemoveDuplicates(ref myFriendsFriends, myFriends);
        }

        public static void ReadFollowees(List<IcollectionUser> followees, List<Follow> topFollow, List<Collection> followeesCollections, int userId, IFollowRepository follow, IcollectionRepository cols)
        {
            foreach (var myFollowee in followees)
            {
                var followeeFollowees = follow.GetFolloweesForUserExcludingMe(myFollowee.Id, userId);
                var myFolloweeCollections = cols.GetICollectionsForThisUser(myFollowee.Id);
                topFollow.AddRange(followeeFollowees);
                followeesCollections.AddRange(myFolloweeCollections);
            }
        }

        public static void OrderLists(ref List<FriendsWith> myFriendsFriends, ref List<Follow> topFollow, ref List<Collection> extractedCollections)
        {
            myFriendsFriends = myFriendsFriends.OrderByDescending(r => r.Began).ToList();
            topFollow = topFollow.OrderByDescending(r => r.Began).ToList();
            extractedCollections = extractedCollections.OrderByDescending(r => r.DateMade).ToList();
        }

        public static string GetMyProfilePicUrl(int myId, IIcollectionUserRepository usersRepo, IPhotoRepository photoRepo)
        {
            string address = "/api/image/thumbnail/";
            int profile_pic_id = usersRepo.GetProfilePicID(myId);
            
            try {
                var guid = photoRepo.GetProfilePicGuid(profile_pic_id);
                string url = address + guid;
                return url;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PhotoInfo> GetMyPhotosInfo(int myId)
        {
            //string address = "https://localhost:5001/api/image/thumbnail/";
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
