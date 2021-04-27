using iCollections.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace iCollections.Data.Abstract
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        List<PhotoInfo> GetMyPhotosInfo(int myId);

        Photo GetPhoto(Guid id);

        Guid GetProfilePicGuid(int myId);

        Task ChangePhotoName(Guid id, string newName);
    }
}