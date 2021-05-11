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
        public JsonResult UploadNewPhoto(IFormFile photo, string name, int userId)
        {
            var uploader = new PhotoUploader(_photoRepo, userId);
            uploader.UploadImage(name, photo);
            return Json(new { success = true });
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> DeletePhoto(Guid photoId)
        {
            var photo = _photoRepo.GetPhoto(photoId);
            await _photoRepo.DeleteAsync(photo);
            return Json(new { success = true });
        }

        [Authorize]
        public void RenamePhoto()
        {

        }

    }
}
