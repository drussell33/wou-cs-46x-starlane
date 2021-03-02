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
using System.IO;


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

            // remove extension
            string nameOfPhoto = _db.Photos.FirstOrDefault(photo => photo.Id == user.ProfilePicId).Name;
            var extension = Path.GetExtension(nameOfPhoto).Replace(".", "");

            // get image
            var profilePic = _db.Photos.FirstOrDefault(photo => photo.Id == user.ProfilePicId);

            // convert bytes to string
            string imageBase64Data = Convert.ToBase64String(profilePic.Data);

            // add extra info to string
            string imageDataURL = string.Format("data:image/{0};base64,{1}", extension,imageBase64Data);

            // putting it all together
            ViewBag.ImageDataUrl = imageDataURL;

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
            var follows = new FollowList { Target = user.UserName, Follows = followers };
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
            var follows = new FollowList { Target = user.UserName, Follows = following };
            return View(follows);
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
    }
}
