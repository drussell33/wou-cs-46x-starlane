using System;
using System.Linq;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Controllers
{
    public class UploadPhotoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public UploadPhotoController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        // Users not logged in who try to upload photos will be redirected to the login page.
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        private int GetICollectionUserID(string id)
        {
            var user = _collectionsDbContext.IcollectionUsers.First(i => i.AspnetIdentityId == id);
            int numericUserId = user.Id;
            return numericUserId;
        }

        [HttpPost]
        public IActionResult UploadImage(string customName)
        {
            string nastyStringId = _userManager.GetUserId(User);
            int userId = DatabaseHelper.GetReadableUserID(nastyStringId, _collectionsDbContext);

            try
            {
                var photoUploader = new PhotoUploader(_collectionsDbContext, userId);
                photoUploader.UploadImage(customName, Request.Form.Files[0]);
                return RedirectToAction("Success");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View("Success", "Your photo was uploaded.");
        }
    }
}
