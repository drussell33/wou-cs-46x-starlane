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
using System.Globalization;


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

        [Route("userpage/{name}")]
        public IActionResult Index(string name)
        {
            string nastyStringId = _userManager.GetUserId(User);
            int userId = DatabaseHelper.GetReadableUserID(nastyStringId, _db);

            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user1 = _db.IcollectionUsers
                .Include("FollowFollowerNavigations")
                .Include("FollowFollowedNavigations")
                .FirstOrDefault(m => m.UserName == name);

            var user2 = _db.IcollectionUsers
                .Include("FollowFollowerNavigations")
                .Include("FollowFollowedNavigations")
                .FirstOrDefault(m => m.Id == userId);

            if (user1 == null || user2 == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            Photo profilePicture = _db.Photos.FirstOrDefault(photo => photo.Id == user.ProfilePicId);
            if (profilePicture != null)
            {
                ViewBag.ImageDataUrl = profilePicture.ToViewableFormat();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        [Route("userpage/{name}/follow")]
        public IActionResult Follow(string id, string status)
        {
            if (id == "")
            {
                return Json(new { success = false, message = "id expected" });
            }
            if (!UserExists(id))
            {
                return Json(new { success = false, message = "User not found" });
            }

            string aspNetUserID = _userManager.GetUserId(User);
            if (aspNetUserID == null)
            {
                return Json(new { success = false, message = "user not logged in" });
            }
            IcollectionUser follower = null;
            if (aspNetUserID != null)
            {
                follower = _db.IcollectionUsers.Where(u => u.AspnetIdentityId == aspNetUserID).FirstOrDefault();
                if (follower == null)
                {
                    return Json(new { success = false, message = "follower not found" });
                }
            }
            IcollectionUser followed = null;
            if (id != null)
            {
                followed = _db.IcollectionUsers.First(f => f.UserName == id);
                if (followed == null)
                {
                    return Json(new { success = false, message = "followed not found" });
                }
            }

            if (status == "new")
            {
                Follow nuFollow = new Follow
                {
                    Followed = followed.Id,
                    Follower = follower.Id,
                    Began = DateTime.Now
                };
                _db.Follows.Add(nuFollow);
                _db.SaveChanges();
                return Json(new { success = true, message = "user was followed" });

            }
            if (status == "following")
            {
                Follow target = _db.Follows.FirstOrDefault(f => f.Followed == followed.Id && f.Follower == follower.Id);
                if (target != null)
                {
                    _db.Follows.Remove(target);
                    _db.SaveChanges();
                }

                return Json(new { success = true, message = "Follow has been removed" });

            }
            
            return Json(new { success = false, message = "error" });
            
        }

        [Route("userpage/{name}/followers")]
        public IActionResult Followers(string name)
        {
            if (name == null )
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Follow> followers = _db.Follows.Include("FollowerNavigation").Where(m => m.FollowedNavigation.UserName == name).ToList();
            var follows = new FollowList { TargetUsername = user.UserName, TargetFirstname = user.FirstName, TargetLastname = user.LastName, Follows = followers };
            return View(follows);
        }

        [Route("userpage/{name}/following")]
        public IActionResult Following(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Follow> following = _db.Follows.Include("FollowedNavigation").Where(m => m.FollowerNavigation.UserName == name).ToList();
            var follows = new FollowList { TargetUsername = user.UserName, TargetFirstname = user.FirstName, TargetLastname = user.LastName, Follows = following.OrderBy(f => f.FollowedNavigation.UserName)};
            return View(follows);
        }

        [HttpPost]
        [Authorize]
        [Route("userpage/{name}/following")]
        public IActionResult Following(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "id expected" });
            }

            if (!FollowExists((int)id))
            {
                return Json(new { success = false, message = "FollowID not found" });
            }

            string aspNetUserID = _userManager.GetUserId(User);

            if (aspNetUserID == null)
            {
                return Json(new { success = false, message = "user not logged in" });
            }
            IcollectionUser user = null;
            if (aspNetUserID != null)
            {
                user = _db.IcollectionUsers.FirstOrDefault(u => u.AspnetIdentityId == aspNetUserID);
                if (user == null)
                {
                    return Json(new { success = false, message = "user not found" });
                }
            }

            //since both follow and user have been verified...
            Follow target = _db.Follows.FirstOrDefault(f => f.Id == id);
            if (target != null)
            {
                _db.Follows.Remove(target);
                _db.SaveChanges();
            }

            return Json(new { success = true, message = "Follow has been removed" });
        }


        [Route("userpage/{name}/collections")]
        public IActionResult Collections(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _db.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var collections = _db.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collections);
        }

        private bool FollowExists(int id)
        {
            return _db.Follows.Any(f => f.Id == id);
        }
        private bool UserExists(String name)
        {
            return _db.IcollectionUsers.Any(f => f.UserName == name);
        }
    }
}
