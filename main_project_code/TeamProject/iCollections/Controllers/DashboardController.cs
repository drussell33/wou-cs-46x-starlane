using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iCollections.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public DashboardController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        private int GetICollectionUserID(string id)
        {
            var user = _collectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == id);
            int numericUserId = user.Id;
            return numericUserId;
        }

        static private IcollectionUser SelectFriend(IcollectionUser user1, IcollectionUser user2, int myId)
        {
            if (user1.Id == myId) return user2;
            return user1;
        }

        private bool KeyInFriendship(IcollectionUser user1, IcollectionUser user2, int key)
        {
            return key == user1.Id || key == user2.Id;
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

        private List<IcollectionUser> GetMyFriends(int myId)
        {
            var myFriendsQuery = _collectionsDbContext.FriendsWiths
                .Include(f => f.User1)
                .Include(f => f.User2)
                .Where(friendship => friendship.User1.Id == myId || friendship.User2.Id == myId)
                .Select(friendship => SelectFriend(friendship.User1, friendship.User2, myId))
                .ToList();

            var myFriends = myFriendsQuery.GroupBy(f => f.Id).Select(f => f.FirstOrDefault()).ToList();
            return myFriends;
        }

        private List<IcollectionUser> GetMyFollowees(int myId)
        {
            var whoIFollow = _collectionsDbContext.Follows
                .Where(f => f.FollowerNavigation.Id == myId)
                .Select(f => f.FollowedNavigation)
                .ToList();

            return whoIFollow;
        }

        // Dashboard opens here - shows a feed of recent events
        [Authorize]
        public IActionResult Index()
        {
            string nastyStringId = _userManager.GetUserId(User);
            int userId = GetICollectionUserID(nastyStringId);
            var myFriends = GetMyFriends(userId);

            List<FriendsWith> myFriendsFriends = new List<FriendsWith>();
            var theirCollections = new List<Collection>();
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

                theirCollections.AddRange(myBuddyCollections);
                myFriendsFriends.AddRange(directFriendsFriend);
            }

            myFriendsFriends.RemoveAll(friendship => KeyInFriendship(friendship.User1, friendship.User2, userId));
            RemoveDuplicates(myFriendsFriends, myFriends);

            var whoIFollow = GetMyFollowees(userId);

            List<Follow> topFollow = new List<Follow>();
            List<Collection> followeesCollections = new List<Collection>();

            foreach (var myFollowee in whoIFollow)
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

            myFriendsFriends = myFriendsFriends.OrderByDescending(r => r.Began).ToList();
            topFollow = topFollow.OrderByDescending(r => r.Began).ToList();
            var actual = followeesCollections.Union(theirCollections).Distinct().ToList();
            actual = actual.OrderByDescending(r => r.DateMade).ToList();

            var activityData = new ActivityEvents
            {
                Me = _userManager.GetUserName(User),
                recentCollections = actual,
                recentFriendships = myFriendsFriends,
                recentFollows = topFollow
            };

            return View(activityData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
