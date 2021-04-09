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
            /*bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //return RedirectToAction("Index", "DashboardController");
                return RedirectToAction("Index", "Dashboard");
            }*/
            // Information straight from the Controller (does not need to do to the database)

/*            // Information straight from the Controller (does not need to do to the database)
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
            }*/


            /*ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an" +
                              $" Admin? {isAdmin}. ID from Identity {id}, email is {email}, and phone is {phone}, and about me is {aboutMe}" +
                              $"Number of followers is {numberOfFollowers} Number of friends is {numberOfFriends}";
            */
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/ocean_environment")]
        public IActionResult Ocean_environment()
        {
            return View();
        }

        [Route("/gallery_environment")]
        public IActionResult gallery_environment()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Users not logged in who try to upload photos will be redirected to the login page.
        [Authorize]
        public IActionResult PhotoUpload()
        {
            return View();
        }
    }
}
