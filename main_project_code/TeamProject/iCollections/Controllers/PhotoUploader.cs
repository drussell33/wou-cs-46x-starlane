using System;
using iCollections.Data;
using iCollections.Models;
using System.IO;
using iCollections.Data.Abstract;

namespace iCollections.Controllers
{
    // All this class does is it uploads a single photo to the database
    public class PhotoUploader
    {
        //private readonly ICollectionsDbContext _collectionsDbContext;
        private IPhotoRepository _photoRepo;
        private int _userId;

        public PhotoUploader(IPhotoRepository photoRepo, int userId)
        {
            //_collectionsDbContext = collectionsDbContext;
            _photoRepo = photoRepo;
            _userId = userId;
        }

        public bool isProperImage(string type)
        {
            return type == "image/jpeg" || type == "image/png" || type == "image/gif";
        }

        public void UploadImage(string customName, Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (customName.Length > 32) { throw new PathTooLongException(); }
            if (isProperImage(file.ContentType))
            {
                Photo photo = new Photo();
                photo.Name = String.IsNullOrEmpty(customName) ? file.FileName : customName;
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                photo.Data = ms.ToArray();
                photo.DateUploaded = DateTime.Now;
                photo.UserId = _userId;
                ms.Close();
                ms.Dispose();
                _photoRepo.AddOrUpdate(photo);
            }
        }   

        public int UploadProfilePicture(string customName, Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file.Length <= 1048576 && isProperImage(file.ContentType))
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
                _photoRepo.AddOrUpdate(photo);
                //_collectionsDbContext.Photos.Add(photo);
                //_collectionsDbContext.SaveChanges();
                return photo.Id;
            }
            if (file.Length > 1048576) { throw new BadImageFormatException("Error: Image is too large to upload"); }
            if (!isProperImage(file.ContentType)) { throw new BadImageFormatException("Error: File uploaded is wrong format"); }
            throw new Exception();
        }
    }
}