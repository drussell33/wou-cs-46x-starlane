using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace iCollections.Data.Concrete
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }

        public List<PhotoInfo> GetMyPhotosInfo(int myId)
        {
            string address = "/api/image/thumbnail/";
            var photosInformation = GetAll()
                                .Where(row => row.User.Id == myId)
                                .Select(myRows => new PhotoInfo { Url = address + myRows.PhotoGuid, PhotoName = myRows.Name })
                                .ToList();

            return photosInformation;
        }

        public Photo GetPhoto(Guid id)
        {
            var photo = GetAll().FirstOrDefault(row => row.PhotoGuid == id);
            return photo;
        }

        public Guid GetProfilePicGuid(int profilePicId)
        {
            return GetAll().FirstOrDefault(p => p.Id == profilePicId).PhotoGuid;
        }

        public async Task ChangePhotoName(Guid id, string newName)
        {
            var selectedPhoto = GetAll().FirstOrDefault(row => row.PhotoGuid == id);
            selectedPhoto.Name = newName;
            await AddOrUpdateAsync(selectedPhoto);
        }

    }
}