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
    /*
        Dashboard Controller (DC):
            what it does:
            Gets ICollection User Id
            Select friend: determines which user out of the two passed in has the key, pass other
            isKeyinFriendship: like select friend but checks if in users
            Removes duplicate friendships in friends query
            Get a user's direct friends
            Get the people a user follows
            Show a feed of recent events

            in a nutshell:
            Querys database
            Passes relevant activity feed info to view
    */
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        private DatabaseHelper dbHelper;

        public DashboardController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
            dbHelper = new DatabaseHelper(_userManager, _collectionsDbContext);
        }

        // Dashboard opens here - shows a feed of recent events
        [Authorize]
        public IActionResult Index()
        {
            string nastyStringId = _userManager.GetUserId(User);
            int userId = dbHelper.GetReadableUserID(nastyStringId);

            var myFriends = dbHelper.GetMyFriends(userId);
            List<FriendsWith> myFriendsFriends = new List<FriendsWith>();
            var theirCollections = new List<Collection>();
            dbHelper.ReadCollectionsAndDistantFriends(myFriends, myFriendsFriends, theirCollections);

            var whoIFollow = dbHelper.GetMyFollowees(userId);
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
