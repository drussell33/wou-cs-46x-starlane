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

        [Route("userpage/{name}")]
        public IActionResult Index(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _db.IcollectionUsers
                .Include("FollowFollowerNavigations")
                .Include("FollowFollowedNavigations")
                .FirstOrDefault(m => m.UserName == name);

            if (user == null)
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
    }
}
