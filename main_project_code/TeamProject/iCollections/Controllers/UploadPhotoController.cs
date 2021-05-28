using System;
using System.Linq;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    public class UploadPhotoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPhotoRepository _photoRepo;
        private readonly IIcollectionUserRepository _userRepo;

        public UploadPhotoController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IPhotoRepository photoRepo, IIcollectionUserRepository userRepo)
        {
            _logger = logger;
            _userManager = userManager;
            _photoRepo = photoRepo;
            _userRepo = userRepo;
        }

        // Users not logged in who try to upload photos will be redirected to the login page.
        [Authorize]
        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null) { ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString(); }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadImage(string customName)
        {
            string aspNetId = _userManager.GetUserId(User);
            int userId = DatabaseHelper.GetReadableUserID(aspNetId, _userRepo);

            try
            {
                var photoUploader = new PhotoUploader(_photoRepo, userId);
                photoUploader.UploadImage(customName, Request.Form.Files[0]);
                TempData["SuccessMessage"] = "Photo uploaded successfully";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Please enter a proper filename");
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View("Success", "Your photo was uploaded.");
        }

        [HttpGet]
        public IActionResult NeedHelp()
        {
            return View();
        }
    }
}
