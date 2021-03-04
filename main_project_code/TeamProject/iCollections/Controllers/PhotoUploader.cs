using System;
using iCollections.Data;
using iCollections.Models;
using System.IO;

namespace iCollections.Controllers
{
    // All this class does is it uploads a single photo to the database
    public class PhotoUploader
    {
        private readonly ICollectionsDbContext _collectionsDbContext;
        private int _userId;

        public PhotoUploader(ICollectionsDbContext collectionsDbContext, int userId)
        {
            _collectionsDbContext = collectionsDbContext;
            _userId = userId;
        }

        private bool isProperImage(string type)
        {
            return type == "image/jpeg" || type == "image/png" || type == "image/gif";
        }

        public void UploadImage(string customName, Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (isProperImage(file.ContentType))
            {
                Photo photo = new Photo();
                photo.Name = (String.IsNullOrEmpty(customName)) ? file.FileName : customName;
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                photo.Data = ms.ToArray();
                photo.DateUploaded = DateTime.Now;
                photo.UserId = _userId;
                ms.Close();
                ms.Dispose();
                _collectionsDbContext.Photos.Add(photo);
                _collectionsDbContext.SaveChanges();
            }
        }
    }
}