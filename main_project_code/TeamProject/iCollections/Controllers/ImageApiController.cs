using System;
using iCollections.Data;
using iCollections.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace iCollections.Controllers
{
    // All this class does is retrieves photo(s)
    public class ImageApiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICollectionsDbContext _collectionsDbContext;

        public ImageApiController(UserManager<IdentityUser> userManager, ICollectionsDbContext collectionsDbContext)
        {
            _userManager = userManager;
            _collectionsDbContext = collectionsDbContext;
        }

        [HttpGet]
        public IActionResult Thumbnail(string id)
        {
            var guid = Guid.Parse(id);
            var databaseReader = new DatabaseHelper(_userManager, _collectionsDbContext);
            var selectedPhoto = databaseReader.GetPhoto(guid);
            var extension = selectedPhoto.GetPhotoExtension();
            // return File(selectedPhoto.Data, $"image/{extension}");
            return File(selectedPhoto.Data, "image/base64");
        }
    }
}