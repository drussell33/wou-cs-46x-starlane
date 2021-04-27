using System;
using iCollections.Data;
using iCollections.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using iCollections.Data.Abstract;


namespace iCollections.Controllers
{
    // All this class does is retrieves photo(s)
    public class ImageApiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IPhotoRepository _photoRepo;

        public ImageApiController(UserManager<IdentityUser> userManager, IPhotoRepository photoRepo)
        {
            _userManager = userManager;
            _photoRepo = photoRepo;
        }

        [HttpGet]
        [ActionName("Thumbnail")]
        public IActionResult GetThumbnail(string id)
        {
            var guid = Guid.Parse(id);
            var selectedPhoto = _photoRepo.GetPhoto(guid);
            return File(selectedPhoto.Data, "image/base64");
        }

        [HttpPost]
        [ActionName("Thumbnail")]
        public IActionResult ChangeThumbnail(string id, string fileName)
        {
            // do work in here ie change the name of the photo
            _photoRepo.ChangePhotoName(Guid.Parse(id), fileName);
            return Content(fileName);
        }
    }
}