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

namespace iCollections.Controllers
{
    public class CreateCollectionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public CreateCollectionController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        [HttpGet]
        public IActionResult EnvironmentSelection()
        {
            TempData["name"] = "Ocean_environment";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnvironmentSelection(CreateCollectionModel collection)
        {
            string name;

            // Testing Hard Temp Data
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            TempData.Keep();

            string id = _userManager.GetUserId(User);
            IcollectionUser appUser = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                TempData["name"] = collection.Route;
                /*Collection newCollection = new Collection();
                newCollection.Route = collection.Route;
                newCollection.UserId = appUser.Id;
                newCollection.DateMade = DateTime.Now;
                newCollection.Visibility = 1;
                newCollection.Name = "unfinished_new_collection";

                _collectionsDbContext.Collections.Add(newCollection);
                _collectionsDbContext.SaveChanges();*/

                return RedirectToAction("PhotoSelection");
            }

            return View("EnvironmentSelection", collection);
           
        }



        [HttpGet]
        public IActionResult PhotoSelection()
        {
            // string Route = route;
            // Testing Hard Temp Data
            string name;
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PhotoSelection(CreateCollectionModel collection)
        {
            Debug.WriteLine(collection);
            if (ModelState.IsValid)
            {
                /*foreach(var photoId in collection.CollectionPhotosIds)
                {
                    //make collection photos from the model form photoids
                }*/
                return RedirectToAction("PublishingOptionsSelection");
            }
            //return View(collection);
            // Testing Hard Temp Data
            string name;
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            TempData.Keep();
            return RedirectToAction("PublishingOptionsSelection");
        }



        [HttpGet]
        public IActionResult PublishingOptionsSelection()
        {
            // Testing Hard Temp Data
            string name;
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            TempData.Keep();

            string[] dropDownList = new string[] { "private", "friends", "public" };
            //var dropDownList = new []string ("high", "low", "none");
            ViewData["Visibility"] = new SelectList(dropDownList);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublishingOptionsSelection(CreateCollectionModel collection)
        {
            string id = _userManager.GetUserId(User);
            IcollectionUser appUser = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();

            // Testing Hard Temp Data
            string name = "nothing";
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            //TempData.Keep();
            Debug.WriteLine(collection);
            if (ModelState.IsValid)
            {
                Collection newCollection = new Collection();
                newCollection.Route = name;
                newCollection.UserId = appUser.Id;
                newCollection.DateMade = DateTime.Now;
                newCollection.Visibility = 1;
                newCollection.Name = "new_collection_with_TEMPDATA";

                _collectionsDbContext.Collections.Add(newCollection);
                _collectionsDbContext.SaveChanges();

                return RedirectToAction("PublishingSuccess");
            }
            string[] dropDownList = new string[] { "private", "friends", "public" };
            //var dropDownList = new []string ("high", "low", "none");
            ViewData["Visibility"] = new SelectList(dropDownList);
            //return View(collection);
            return RedirectToAction("PublishingSuccess");
        }

        [HttpGet]
        public IActionResult PublishingSuccess()
        {
            // Testing Hard Temp Data
           /* string name;
            if (TempData.ContainsKey("name"))
                name = TempData["name"].ToString(); // returns "Bill" 
            TempData.Keep();*/

            string id = _userManager.GetUserId(User);
            IcollectionUser appUser = _collectionsDbContext.IcollectionUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();
            var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == appUser.UserName).Collections;
            //var collections = _collectionsDbContext.IcollectionUsers.FirstOrDefault(m => m.Id == appUser.Id).Collections;
            return View();
        }
    }
}
