using iCollections.Data;
using iCollections.Data.Abstract;
using iCollections.Models;
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


        public CollectionsController(ILogger<CollectionsController> logger, UserManager<IdentityUser> userManager, IIcollectionUserRepository userRepo, ICollectionRepository collectionRepo, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;

            _userRepo = userRepo;
            _collectionRepo = collectionRepo;

        }


        public async Task<IActionResult> MyCollectionsPage()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            return View();
        }


        [Route("Collections/{name}")]
        public IActionResult Collections(string name)
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
            var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collections);
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

            var collections = _collectionsDbContext.IcollectionUsers.Include("Collections").FirstOrDefault(m => m.UserName == name).Collections;
            return View(collections);
        }
    }
}
