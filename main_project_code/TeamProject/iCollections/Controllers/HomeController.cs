using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iCollections.Data;

namespace iCollections.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        private ICollectionsDbContext collectionsDb;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            collectionsDb = db;
        }

        public async Task<IActionResult> Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //return RedirectToAction("Index", "DashboardController");
                return RedirectToAction("Index", "Dashboard");
            }
            /*            // Information straight from the Controller (does not need to do to the database)
                        bool isAdmin = User.IsInRole("Admin");
                        bool isAuthenticated = User.Identity.IsAuthenticated;
                        string name = User.Identity.Name;
                        string authType = User.Identity.AuthenticationType;

                        // Information from Identity through the user manager
                        string id = _userManager.GetUserId(User);         // reportedly does not need to hit db
                        IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
                        string email = user?.Email ?? "no email";
                        string phone = user?.PhoneNumber ?? "no phone number";
                        ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an" +
                                          $" Admin? {isAdmin}. ID from Identity {id}, email is {email}, and phone is {phone}";*/
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

        public int GetICollectionUserID(string id) {
            // get user with id
            var user = collectionsDb.IcollectionUsers.First(i => i.AspnetIdentityId == id);
            int numericUserId = user.Id;
            // get numerical id and return
            return numericUserId;
        }

        // Dashboard opens here - shows a feed of recent events
        public IActionResult Feed()
        {
            // query db for events (who followed who, friends, collections)
            // string nastyStringId = _userManager.GetUserId(User);
            // int userId = GetICollectionUserID(nastyStringId);
            int userId = 2;     // hardcoded userId
            // need some way to access iCollection db like a db context of some kind
            // access user with userid
            // Console.WriteLine(collectionsDb.IcollectionUsers.First(i => i.Id == userId).FirstName);
            
            // get all my user's collections
            var accountCollections = collectionsDb.Collections
                    .Where(collection => collection.UserId == userId);
            // foreach (var item in accountCollections)
            // {
            //     Console.WriteLine(item.Name);
            // }

            // we need to sort list of events by date (most recent first)
            // events defined above
            // get the events

            return View();
        }
    }
}
