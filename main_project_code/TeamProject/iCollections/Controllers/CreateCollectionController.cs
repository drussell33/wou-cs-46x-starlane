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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class CreateCollectionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly ICollectionsDbContext _collectionsDbContext;

        private readonly IIcollectionUserRepository _userRepo;
        private readonly IPhotoRepository _photoRepo;
        private readonly IcollectionRepository _colRepo;
        private readonly ICollectionPhotoRepository _collectionPhotoRepo;
        private readonly ICollectionKeywordRepository _collectionKeywords;
        private readonly IKeywordRepository _keywordRepo;

        public CreateCollectionController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, /*ICollectionsDbContext collectionsDbContext,*/ IIcollectionUserRepository userRepo, IPhotoRepository photoRepo, IcollectionRepository colRepo, ICollectionPhotoRepository collectionphotoRepo, ICollectionKeywordRepository collectionKeywords, IKeywordRepository keywords)
        {
            _logger = logger;
            _userManager = userManager;
            //_collectionsDbContext = collectionsDbContext;
            _userRepo = userRepo;
            _photoRepo = photoRepo;
            _colRepo = colRepo;
            _collectionPhotoRepo = collectionphotoRepo;
            _collectionKeywords = collectionKeywords;
            _keywordRepo = keywords;
        }
        [Authorize]
        [HttpGet]
        public IActionResult EnvironmentSelection()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EnvironmentSelection([Bind("Route")] CreateCollectionRoute collection)
        {
            if (ModelState.IsValid)
            {
                ViewData["routeName"] = collection.Route;

                //Add in or route length > 1
                if (collection.Route != "false")
                {
                    TempData["route"] = collection.Route;

                    return RedirectToAction("PhotoSelection");
                }
            }
            return View("EnvironmentSelection");
        }


        [Authorize]
        [HttpGet]
        public IActionResult PhotoSelection()
        {
            TempData.Keep();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult PhotoSelection(string[] selectedPhotos)
        {
            //Add in the ability to give a title and description for the photo to be used in the collection
            if (ModelState.IsValid)
            {
                ViewData["errorPresent"] = true;

                string selectedRoute = TempData["route"].ToString();

                if (selectedRoute == "ocean_environment" && selectedPhotos.Length > 0 && selectedPhotos.Length < 9)
                {
                    TempData["photoids"] = selectedPhotos;
                    ViewData["errorPresent"] = null;
                    TempData.Keep();
                    return RedirectToAction("PublishingOptionsSelection");
                }

                if (selectedRoute == "gallery_environment" && selectedPhotos.Length > 0 && selectedPhotos.Length < 40)
                {
                    TempData["photoids"] = selectedPhotos;
                    ViewData["errorPresent"] = null;
                    TempData.Keep();
                    return RedirectToAction("PublishingOptionsSelection");
                }


            }

            TempData.Keep();
            return View("PhotoSelection", selectedPhotos);
        }


        [Authorize]
        [HttpGet]
        public IActionResult PublishingOptionsSelection()
        {
            TempData.Keep();

            // figure out how to do privacy options
            string[] dropDownList = new string[] { "Public", "Private"};
            ViewData["Visibility"] = new SelectList(dropDownList);
            var activeKeywords = _keywordRepo.GetAll();
            ViewData["KeywordsAvailable"] = new SelectList(activeKeywords, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PublishingOptionsSelection([Bind("CollectionName", "Visibility", "Description", "SelectedKeyword")]CreateCollectionPublishing collection)
        {
            string id = _userManager.GetUserId(User);
            //IcollectionUser appUser = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();
            IcollectionUser appUser = null;
            if (id != null)
            {
                appUser = _userRepo.GetSessionUser(id);
            }

           // Get route selection from tempdata cookie
           string route = "nothing";
            if (TempData.ContainsKey("route"))
            {
                route = TempData["route"].ToString();
            }
            //TempData.Keep();
            Debug.WriteLine(collection);
            if (ModelState.IsValid)
            {
                Collection newCollection = new Collection();
                newCollection.Route = route;
                newCollection.UserId = appUser.Id;
                newCollection.DateMade = DateTime.Now;
                newCollection.Visibility = 0;
                if (collection.Visibility == "Public")
                {
                    newCollection.Visibility = 1;
                }
                newCollection.Name = collection.CollectionName;
                newCollection.Description = collection.Description;

                //_collectionsDbContext.Collections.Add(newCollection);
                //await _collectionsDbContext.SaveChangesAsync();
                await _colRepo.AddOrUpdateAsync(newCollection);


                // Get photo selection from tempdata cookie
                if (TempData.ContainsKey("photoids"))
                {
                    var objectArray = (string[])TempData["photoids"];
                    if(objectArray != null)
                    {
                        for (var i = 0; i < objectArray.Length; i++)
                        {
                            //foreach (var photo in _collectionsDbContext.Photos.Where(u => u.UserId == appUser.Id).ToList())
                            foreach (var photo in _photoRepo.GetAllUserPhotos(appUser.Id))
                            {
                                if (objectArray[i].ToString() == photo.Id.ToString())
                                {
                                    CollectionPhoto newCollectionPhoto = new CollectionPhoto();
                                    string idConvert = objectArray[i].ToString();
                                    newCollectionPhoto.PhotoId = Int32.Parse(idConvert);
                                    newCollectionPhoto.CollectId = newCollection.Id;
                                    newCollectionPhoto.PhotoRank = 1;
                                    newCollectionPhoto.DateAdded = DateTime.Now;
                                    newCollectionPhoto.Title = "new title";
                                    newCollectionPhoto.Description = "new description";
                                    //_collectionsDbContext.CollectionPhotos.Add(newCollectionPhoto);
                                    await _collectionPhotoRepo.AddOrUpdateAsync(newCollectionPhoto);
                                    //await _collectionsDbContext.SaveChangesAsync();
                                }
                            }
                        }


                        CollectionKeyword addingKeyword = new CollectionKeyword();
                        addingKeyword.CollectId = newCollection.Id;
                        addingKeyword.KeywordId = collection.SelectedKeyword;
                        await _collectionKeywords.AddOrUpdateAsync(addingKeyword);

                    }
                    
                }
                TempData.Remove("photoids");
                TempData.Remove("route");
                TempData.Clear();
                return RedirectToAction("PublishingSuccess");
            }
            string[] dropDownList = new string[] { "private", "friends", "public" };
            ViewData["Visibility"] = new SelectList(dropDownList);
            var activeKeywords = _keywordRepo.GetAll();
            ViewData["KeywordsAvailable"] = new SelectList(activeKeywords, "Id", "Name");

            return View("PublishingOptionsSelection", collection);
        }

        [Authorize]
        [HttpGet]
        public IActionResult PublishingSuccess()
        {

            string id = _userManager.GetUserId(User);
            //IcollectionUser appUser = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();
            IcollectionUser appUser = null;
            if (id != null)
            {
                appUser = _userRepo.GetSessionUser(id);
            }
            //var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == appUser.UserName).Collections.OrderByDescending(c => c.DateMade);
            var collections = _colRepo.GetMostRecentiCollections(appUser.Id, 10);

            return View(collections);
        }
    }
}
