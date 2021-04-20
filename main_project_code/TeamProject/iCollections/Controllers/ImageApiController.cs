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
        [ActionName("Thumbnail")]
        public IActionResult GetThumbnail(string id)
        {
            var guid = Guid.Parse(id);
            var databaseReader = new DatabaseHelper(_userManager, _collectionsDbContext);
            var selectedPhoto = databaseReader.GetPhoto(guid);
            var extension = selectedPhoto.GetPhotoExtension();
            // return File(selectedPhoto.Data, $"image/{extension}");
            return File(selectedPhoto.Data, "image/base64");
        }

        [HttpPost]
        [ActionName("Thumbnail")]
        public string ChangeThumbnail(string id, string fileName)
        {
            // do work in here ie change the name of the photo
            Console.WriteLine(fileName);
            DatabaseEditor databaseEditor = new DatabaseEditor(_userManager, _collectionsDbContext);
            databaseEditor.ChangePhotoName(Guid.Parse(id), fileName);
            return fileName;
        }
    }
}