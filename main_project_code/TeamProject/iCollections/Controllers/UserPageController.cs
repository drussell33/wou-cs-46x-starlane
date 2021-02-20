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

namespace iCollections.Controllers
{
    public class UserPageController : Controller
    {
        private readonly ICollectionsDbContext _db;

        public UserPageController(ICollectionsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int id)
        {
            var user = _db.IcollectionUsers.FirstOrDefault(m => m.Id == id);
            return View(user);
        }

        public IActionResult Followers(int id)
        {
            var followers = _db.Follows.Include("FollowerNavigation").Where(m => m.Followed == id).ToList();
            var user_followers = new List<IcollectionUser>();
            foreach (var follower in followers) {
                user_followers.Add(follower.FollowerNavigation);
            }
            return View(user_followers);
        }

        public IActionResult Following(int id)
        {
            var following = _db.Follows.Include("FollowedNavigation").Where(m => m.Follower == id).ToList();
            var user_followed = new List<IcollectionUser>();
            foreach (var follow in following)
            {
                user_followed.Add(follow.FollowerNavigation);
            }
            return View(user_followed);
        }

        public IActionResult Collections(int id)
        {
            var collections = _db.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.Id == id).Collections;
            return View(collections);
        }
    }
}
