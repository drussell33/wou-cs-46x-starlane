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
            string sessionUserId = _userManager.GetUserId(User);
            IcollectionUser sessionUser = null;

            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (sessionUserId != null)
            {
                sessionUser = _db.IcollectionUsers
                .Include(u => u.FollowFollowerNavigations)
                .Include(u => u.FollowFollowedNavigations)
                .FirstOrDefault(m => m.AspnetIdentityId == sessionUserId);
            }

            var targetUser = _db.IcollectionUsers
                .Include(u => u.FollowFollowerNavigations)
                .Include(u => u.FollowFollowedNavigations)
                .Include(u => u.Photos)
                .FirstOrDefault(m => m.UserName == name);
            
            return View(new UserProfile { ProfileVisitor = sessionUser, ProfileOwner = targetUser });
        }

        [Authorize]
        [Route("api/sessionuser")]
        public async Task<JsonResult> GetUserNameFromAspId()
        {
            var sessionUser = _userManager.GetUserId(User);
            var username = await _db.IcollectionUsers.FirstOrDefaultAsync(x => x.AspnetIdentityId == sessionUser );
            if (username == null)
            {
                return Json(new { username = "" });
            }
            return Json(new { username = username.UserName, id = username.Id });
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
        public async Task<JsonResult> Following(int? id)
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
                await _db.SaveChangesAsync();
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
