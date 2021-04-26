using iCollections.Data;
using iCollections.Data.Abstract;
using iCollections.Models;
using iCollections.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ILogger<CollectionsController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        private readonly IIcollectionUserRepository _userRepo;
        private readonly ICollectionRepository _collectionRepo;
        private readonly IPhotoRepository _photoRepo;


        public CollectionsController(ILogger<CollectionsController> logger, UserManager<IdentityUser> userManager, IIcollectionUserRepository userRepo, ICollectionRepository collectionRepo, IPhotoRepository photoRepo, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;

            _userRepo = userRepo;
            _collectionRepo = collectionRepo;
            _photoRepo = photoRepo;


        }


        public async Task<IActionResult> MyCollectionsPage()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }


        [Route("Collections/{name}")]
        public IActionResult Collections(string name, string keywords)
        {
            if (keywords == null)
            {
                if (name == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                IcollectionUser user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                IcollectionUser active_user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(u => u.AspnetIdentityId == _userManager.GetUserId(User));

                BrowseList collectionlist = new BrowseList
                {
                    LoggedInUser = active_user,
                    VisitedUser = user,
                    SearchResults = _collectionsDbContext.CollectionKeywords.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).ToList(),
                    SuggestedKeywords = null

                };

                var myId = _userRepo.GetReadableUserID(name);
                ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(myId, _userRepo, _photoRepo);
                //var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;



                return View(collectionlist);
            }
            else 
            {
                if (name == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                IcollectionUser user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                string[] keys = StringUtilities.SplitBySpace(keywords); // parse strings separated by space or whitespace
                List<CollectionKeyword> filtered = new List<CollectionKeyword>();
                foreach (string token in keys)
                {
                    var coll_keys = _collectionsDbContext.CollectionKeywords.Include(c => c.Collect).Include(k => k.Keyword).Where(c => c.Collect.User == user && c.Keyword.Name.Contains(token)).ToList();

                    filtered.AddRange(coll_keys);

                }

                filtered.Union(filtered);

                IcollectionUser active_user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(u => u.AspnetIdentityId == _userManager.GetUserId(User));

                BrowseList collectionlist = new BrowseList
                {
                    LoggedInUser = active_user,
                    VisitedUser = user,
                    SearchResults = filtered,
                    SuggestedKeywords = null

                };

                var myId = _userRepo.GetReadableUserID(name);
                ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(myId, _userRepo, _photoRepo);

                return View(collectionlist);
            }
            
        }

        [Authorize]
        [Route("Collections/{name}/MyCollections")]
        public IActionResult MyCollections(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IcollectionUser user = _collectionsDbContext.IcollectionUsers.FirstOrDefault(m => m.UserName == name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string sessionusername = _userManager.GetUserId(User);
            if (sessionusername != user.AspnetIdentityId)
            {
                return RedirectToAction("Index", "Home");
            }

            BrowseList collectionlist = new BrowseList
            {
                LoggedInUser = user,
                VisitedUser = user,
                SearchResults = _collectionsDbContext.CollectionKeywords.Include(ck => ck.Keyword).Include(c => c.Collect).Where(c => c.Collect.User == user).ToList(),
                SuggestedKeywords = null
                
            };

            //var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collectionlist);
        }
    }
}
