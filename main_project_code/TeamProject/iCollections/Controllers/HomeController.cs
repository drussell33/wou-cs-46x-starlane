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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        public async Task<IActionResult> Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //return RedirectToAction("Index", "DashboardController");
                return RedirectToAction("Index", "Dashboard");
            }
            // Information straight from the Controller (does not need to do to the database)

            // Information straight from the Controller (does not need to do to the database)
            bool isAdmin = User.IsInRole("Admin");
            string name = User.Identity.Name;
            string authType = User.Identity.AuthenticationType;

            // Information from Identity through the user manager
            string id = _userManager.GetUserId(User);         // reportedly does not need to hit db
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            string email = user?.Email ?? "no email";
            string phone = user?.PhoneNumber ?? "no phone number";
            IcollectionUser cu = null;
            int numberOfFollowers = 0;
            int numberOfFriends = 0;
            string aboutMe = null;
            if (id != null)
            {
                cu = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();

                aboutMe = cu?.AboutMe ?? "no about me";
                numberOfFollowers = _collectionsDbContext.Follows.Where(u => u.Followed == cu.Id).Count();
                numberOfFriends = _collectionsDbContext.FriendsWiths.Where(u => u.User1Id == cu.Id).Count();
            }


            ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an" +
                              $" Admin? {isAdmin}. ID from Identity {id}, email is {email}, and phone is {phone}, and about me is {aboutMe}" +
                              $"Number of followers is {numberOfFollowers} Number of friends is {numberOfFriends}";
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int GetICollectionUserID(string id)
        {
            var user = _collectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == id);
            int numericUserId = user.Id;
            return numericUserId;
        }

        static public IcollectionUser SelectFriend(IcollectionUser user1, IcollectionUser user2, int myId)
        {
            if (user1.Id == myId) return user2;
            return user1;
        }

        public bool KeyInFriendship(IcollectionUser user1, IcollectionUser user2, int key)
        {
            return key == user1.Id || key == user2.Id;
        }

        public void RemoveDuplicates(List<FriendsWith> list, List<IcollectionUser> directFriends)
        {
            for (int i = list.Count() - 1; i >= 0; i--)
            {
                var user1 = list[i].User1.Id;
                var user2 = list[i].User2.Id;
                // list.RemoveAll(r => r.User1.Id == id2 && r.User2.Id == id);
                if (directFriends.Any(myBuddy => myBuddy.Id == user2)) list.RemoveAll(r => r.User1.Id == user1 && r.User2.Id == user2);
                else list.RemoveAll(r => r.User1.Id == user2 && r.User2.Id == user1);
            }
        }

        public List<IcollectionUser> GetMyFriends(int myId)
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

        public List<IcollectionUser> GetMyFollowees(int myId)
        {
            var whoIFollow = _collectionsDbContext.Follows
                .Where(f => f.FollowerNavigation.Id == myId)
                .Select(f => f.FollowedNavigation)
                .ToList();

            return whoIFollow;
        }

        // Dashboard opens here - shows a feed of recent events
        public IActionResult Feed()
        {
            // string nastyStringId = _userManager.GetUserId(User);
            // int userId = GetICollectionUserID(nastyStringId);
            int userId = 2;     // my hardcoded userId

            // get all events and order each list by date using .OrderBy(c => c.Date)

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
                recentCollections = actual,
                recentFriendships = myFriendsFriends,
                recentFollows = topFollow
            };

            // in View - loop through 3 lists at same time "popping" the most recent event from
            // its list and render its info

            return View(activityData);
        }

        // Users not logged in who try to upload photos will be redirected to the login page.
        [Authorize]
        public IActionResult PhotoUpload()
        {
            return View();
        }
    }
}
