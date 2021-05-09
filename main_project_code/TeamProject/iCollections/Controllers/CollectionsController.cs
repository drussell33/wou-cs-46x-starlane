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


        private readonly IIcollectionUserRepository _userRepo;
        private readonly ICollectionKeywordRepository _collectionkeywordRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly IFavoriteCollectionRepository _favoritecollectionRepo;


        public CollectionsController(ILogger<CollectionsController> logger, UserManager<IdentityUser> userManager, IIcollectionUserRepository userRepo, ICollectionKeywordRepository collectionkeywordRepo, IPhotoRepository photoRepo, IFavoriteCollectionRepository favoritecollectionRepo)
        {
            _logger = logger;
            _userManager = userManager;

            _userRepo = userRepo;
            _collectionkeywordRepo = collectionkeywordRepo;
            _photoRepo = photoRepo;
            _favoritecollectionRepo = favoritecollectionRepo;

        }

        [Route("Collections/{name}")]
        public IActionResult Collections(string name, string keywords, string sort)
        {
            //Initial state, no sort or search requested.
            if (keywords == null && sort == null)
            {
                if (name == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                IcollectionUser user = _userRepo.GetIcollectionUserByUsername(name);

                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                IcollectionUser active_user = _userRepo.GetIcollectionUserByIdentityId(_userManager.GetUserId(User));

                BrowseList collectionlist = new BrowseList
                {
                    LoggedInUser = active_user,
                    VisitedUser = user,
                    SearchResults = _collectionkeywordRepo.GetCollectionKeywordsByUser(user),
                    SuggestedKeywords = null

                };

                var myId = _userRepo.GetReadableID(name);
                //ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(myId, _userRepo, _photoRepo);

                return View(collectionlist);
            }

            //Sort requested no search
            if (keywords == null)
            {
                if (name == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                IcollectionUser user = _userRepo.GetIcollectionUserByUsername(name);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                IcollectionUser active_user = _userRepo.GetIcollectionUserByIdentityId(_userManager.GetUserId(User));

                List<CollectionKeyword> sorted = _collectionkeywordRepo.GetCollectionKeywordsByUserSortedAscending(user, sort);

                BrowseList collectionlist = new BrowseList
                {

                    LoggedInUser = active_user,
                    VisitedUser = user,
                    SearchResults = sorted,
                    SuggestedKeywords = null

                };

                var myId = _userRepo.GetReadableID(name);
                //ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(myId, _userRepo, _photoRepo);

                return View(collectionlist);
            }

            //Both sort and search requested
            else
            {
                if (name == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                IcollectionUser user = _userRepo.GetIcollectionUserByUsername(name);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                string[] keys = StringUtilities.SplitBySpace(keywords); // parse strings separated by space or whitespace
                List<CollectionKeyword> filtered = new List<CollectionKeyword>();
                foreach (string token in keys)
                {
                    var coll_keys = _collectionkeywordRepo.GetUserCollectionKeywordsByKeyword(user, token);

                    filtered.AddRange(coll_keys);
                }

                filtered.Union(filtered);

                List<CollectionKeyword> sorted = new List<CollectionKeyword>();
                if (sort == "name")
                {
                    sorted = filtered.OrderBy(o => o.Collect.Name).ToList();
                }

                else if (sort == "keyword")
                {
                    sorted = filtered.OrderBy(o => o.Keyword.Name).ToList();
                }

                else if (sort == "date")
                {
                    sorted = filtered.OrderBy(o => o.Collect.DateMade).ToList();
                }

                IcollectionUser active_user = _userRepo.GetIcollectionUserByIdentityId(_userManager.GetUserId(User));

                BrowseList collectionlist = new BrowseList
                {
                    LoggedInUser = active_user,
                    VisitedUser = user,
                    SearchResults = sorted,
                    SuggestedKeywords = null

                };

                var myId = _userRepo.GetReadableID(name);
                //ViewBag.ProfilePicUrl = DatabaseHelper.GetMyProfilePicUrl(myId, _userRepo, _photoRepo);

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
            IcollectionUser user = _userRepo.GetIcollectionUserByUsername(name);
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
                SearchResults = _collectionkeywordRepo.GetCollectionKeywordsByUser(user),
                SuggestedKeywords = null
                
            };

            return View(collectionlist);
        }

        [Authorize]
        [HttpPost]
        [Route("Collections/{name}/AddFavorite")]
        public IActionResult AddFavorite(int collection, string visiteduser, string activeuser)
        {
            string result = "";
            IcollectionUser loggedinuser = _userRepo.GetIcollectionUserByUsername(activeuser);
            FavoriteCollection favorites = _favoritecollectionRepo.GetMyFavoritesByUser(loggedinuser);
            if (favorites == null)
            {
                
            }
            
            System.Console.WriteLine(collection);
            System.Console.WriteLine(visiteduser);
            System.Console.WriteLine(activeuser);

            return Json(new { activeuser, collection });
        }

        
    }
}
