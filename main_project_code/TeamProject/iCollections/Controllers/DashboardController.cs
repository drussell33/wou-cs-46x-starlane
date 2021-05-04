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
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;
        private readonly IFriendsWithRepository _friends;
        private readonly IcollectionRepository _collections;
        private readonly IFollowRepository _follow;
        private readonly IIcollectionUserRepository _users;
        private readonly IPhotoRepository _photos;

        public DashboardController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext,
            IFriendsWithRepository friends, IcollectionRepository collections, IFollowRepository follow, IIcollectionUserRepository users, IPhotoRepository photos)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
            _friends = friends;
            _collections = collections;
            _follow = follow;
            _users = users;
            _photos = photos;
        }

        // Dashboard opens here - shows a feed of recent events
        [Authorize]
        public IActionResult Index()
        {
            // Determine user
            string nastyStringId = _userManager.GetUserId(User);
            int userId = DatabaseHelper.GetReadableUserID(nastyStringId, _collectionsDbContext);
            string userName = DatabaseHelper.GetICollectionUserName(nastyStringId, _collectionsDbContext);

            // start querying distants and my friends' collections
            var myFriends = _friends.GetMyFriends(userId);
            List<FriendsWith> myFriendsFriends = new List<FriendsWith>();
            var friendsCollections = new List<Collection>();
            DatabaseHelper.ReadDistantFriends(myFriends, ref myFriendsFriends, friendsCollections, userId, _friends, _collections);

            // start querying distant followees and my followees' collections
            var whoIFollow = _follow.GetMyFollowees(userId);
            List<Follow> topFollow = new List<Follow>();
            List<Collection> followeesCollections = new List<Collection>();
            DatabaseHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId, _follow, _collections);

            // Gather remaining lists and order them chronologically
            var extractedCollections = followeesCollections.Union(friendsCollections).Distinct().ToList();
            DatabaseHelper.OrderLists(ref myFriendsFriends, ref topFollow, ref extractedCollections);
            ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(userId, _users, _photos);

            var activityData = new ActivityEvents
            {
                MyEmail = _userManager.GetUserName(User),
                MyUsername = userName,
                recentCollections = extractedCollections,
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
