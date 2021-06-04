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
using Microsoft.AspNetCore.Http;
using System.IO;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class UserPageController : Controller
    {
        private readonly ICollectionsDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IIcollectionUserRepository _userRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly IcollectionRepository _colRepo;

        public UserPageController(ICollectionsDbContext db, UserManager<IdentityUser> userManager, IIcollectionUserRepository userRepo, IPhotoRepository photoRepo, IcollectionRepository colRepo)
        {
            _db = db;
            _userManager = userManager;
            _userRepo = userRepo;
            _photoRepo = photoRepo;
            _colRepo = colRepo;
        }

        [Route("userpage/{name}")]
        public IActionResult Index(string name)
        {
            string sessionUserId = _userManager.GetUserId(User);
            IcollectionUser sessionUser = null;
            var profileId = _userRepo.GetReadableID(name);
            ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(profileId, _userRepo, _photoRepo);

            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrEmpty(sessionUserId))
            {
                sessionUser = _userRepo.GetSessionUser(sessionUserId);
            }

            var targetUser = _userRepo.GetTargetUser(name);

            if (targetUser == null)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { StatusCode = 404, Message = $"User {name} was not found. Try using the search bar!" });
            }
            Console.WriteLine("before getting collections");
            var recentiCollections = _colRepo.GetMostRecentiCollections(profileId, 4);
            Console.WriteLine("after getting collections");            
            return View(new UserProfile { ProfileVisitor = sessionUser, ProfileOwner = targetUser, recentCollections = recentiCollections });
        }

        [Route("userpage/{name}/followers")]
        public IActionResult Followers(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _db.IcollectionUsers
                                    .Include(x => x.FollowFollowedNavigations)
                                    .ThenInclude(f => f.FollowerNavigation)
                                    .FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Follow> followers = user.FollowFollowedNavigations.ToList();
            return View(new FollowList { TargetUser = user, Follows = followers });
        }

        [Route("userpage/{name}/following")]
        public IActionResult Following(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _db.IcollectionUsers
                                    .Include(x => x.FollowFollowerNavigations)
                                    .ThenInclude(f => f.FollowedNavigation)
                                    .FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Follow> following = user.FollowFollowerNavigations.ToList();
            return View(new FollowList { TargetUser = user, Follows = following });
        }

        [Authorize]
        [Route("userpage/{name}/edit")]
        public IActionResult Edit(string name)
        {
            var user = _userRepo.GetTargetUser(name);

            //string sessionUserId = _userManager.GetUserId(User);
            //IcollectionUser sessionUser = null;
            var profileId = _userRepo.GetReadableID(name);
            ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(profileId, _userRepo, _photoRepo);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.AspnetIdentityId == _userManager.GetUserId(User))
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [Route("userpage/{name}/edit")]
        [HttpPost]
        public IActionResult EditPost(IcollectionUser fu, int id, IFormFile profileimg)
        {
            var user = _userRepo.GetUserById(id);
            //var user = _db.IcollectionUsers.FirstOrDefault(u => u.Id == id);
            if (ModelState.IsValid)
            {
                Console.WriteLine("Valid");
                if (user != null && fu.UserName != null && fu.FirstName != null && fu.LastName != null && fu.AboutMe != null)
                {
                    if (!_userRepo.Exists(fu.UserName))
                    {
                        user.UserName = fu.UserName;
                    }
                    user.FirstName = fu.FirstName;
                    user.LastName = fu.LastName;
                    user.AboutMe = fu.AboutMe;

                    if (profileimg != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        profileimg.CopyTo(ms);
                        var profileImg = new Photo { Name = profileimg.FileName, Data = ms.ToArray(), DateUploaded = DateTime.Now, UserId = user.Id };
                        ms.Close();
                        ms.Dispose();
                        //_db.Photos.Add(profileImg);
                        _photoRepo.AddOrUpdate(profileImg);
                        //_db.SaveChanges();
                        user.ProfilePicId = profileImg.Id;
                    }
                    _userRepo.AddOrUpdate(user);
                    //_db.IcollectionUsers.Update(user);
                    //_db.SaveChanges();
                }
                return RedirectToAction("Index", "UserPage", new { name = user.UserName });
            }
            else
            {
                Console.WriteLine("Not Valid");
                return RedirectToAction("Index", "UserPage", new { name = user.UserName });
            }
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
