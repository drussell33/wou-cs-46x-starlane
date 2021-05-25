using System;
using System.Linq;
using iCollections.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iCollections.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iCollections.Data.Abstract;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace iCollections.Controllers
{
    public class ViewPhotosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPhotoRepository _photoRepo;
        private readonly IIcollectionUserRepository _userRepo;

        public ViewPhotosController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IPhotoRepository photoRepo, IIcollectionUserRepository userRepo)
        {
            _logger = logger;
            _userManager = userManager;
            _photoRepo = photoRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            string userAspId = _userManager.GetUserId(User);
            int userId = _userRepo.GetReadableUserID(userAspId);
            var photos = _photoRepo.GetMyPhotosInfo(userId);
            return View(photos);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadNewPhoto(IFormFile photo, string name)
        {
            string userAspId = _userManager.GetUserId(User);
            int userId = _userRepo.GetReadableUserID(userAspId);
            var photos = _photoRepo.GetMyPhotosInfo(userId);
            var uploader = new PhotoUploader(_photoRepo, userId);
            if (uploader.isProperImage(photo.ContentType))
            {
                if (String.IsNullOrEmpty(name))
                {
                    uploader.UploadImage(photo.FileName, photo);
                }
                else
                {
                    try
                    {
                        uploader.UploadImage(name, photo);
                    }
                    catch (Exception) {
                        ModelState.AddModelError(String.Empty, "Please enter a proper filename");
                        return View("Index", photos);
                    }
                }
            } 
            else
            {
                ModelState.AddModelError(String.Empty, "Pictures may be of type .jpeg, .png, or .gif");
                return View("Index", photos);
            }
            ModelState.AddModelError(String.Empty, "Something happened");
            return RedirectToAction("Index", photos);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/deletePhoto")]
        public async Task<JsonResult> DeletePhoto(string photoId)
        {
            var photo = _photoRepo.GetPhoto(Guid.Parse(photoId));
            if (photo.UserId != DatabaseHelper.GetReadableUserID(_userManager.GetUserId(User), _userRepo))
            {
                return Json(new { success = false });
            }
            await _photoRepo.DeleteAsync(photo);   
            return Json(new { success = true });
        }
    }
}
