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
        // private readonly ICollectionsDbContext _collectionsDbContext;

        private readonly IPhotoRepository _photoRepo;

        public ImageApiController(UserManager<IdentityUser> userManager, IPhotoRepository photoRepo)
        {
            _userManager = userManager;
            // _collectionsDbContext = collectionsDbContext;
            _photoRepo = photoRepo;
        }

        [HttpGet]
        public IActionResult Thumbnail(string id)
        {
            var guid = Guid.Parse(id);
            var selectedPhoto = _photoRepo.GetPhoto(guid);
            return File(selectedPhoto.Data, "image/base64");
        }
    }
}