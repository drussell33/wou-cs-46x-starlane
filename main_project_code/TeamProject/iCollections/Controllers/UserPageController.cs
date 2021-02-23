using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iCollections.Data;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Controllers
{
    public class UserPageController : Controller
    {
        private readonly ICollectionsDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UserPageController(ICollectionsDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string name)
        {
            // Redirect to main page if no id
            if (name is null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Return view
            else
            {
                var user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
                return View(user);
            }
        }

        public IActionResult Followers(string name)
        {
            var user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            var followers = _db.Follows.Include("FollowerNavigation").Where(m => m.Followed == user.Id).ToList();
            var user_followers = new List<IcollectionUser>();
            foreach (var follower in followers) {
                user_followers.Add(follower.FollowerNavigation);
            }
            return View(user_followers);
        }

        public IActionResult Following(string name)
        {
            var user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            var following = _db.Follows.Include("FollowedNavigation").Where(m => m.Follower == user.Id).ToList();
            var user_followed = new List<IcollectionUser>();
            foreach (var follow in following)
            {
                user_followed.Add(follow.FollowerNavigation);
            }
            return View(user_followed);
        }

        public IActionResult Collections(string name)
        {
            var collections = _db.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collections);
        }
    }
}
