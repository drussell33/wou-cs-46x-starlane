using iCollections.Models;
using System.Collections.Generic;
using System;

namespace iCollections.Data.Abstract
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        List<PhotoInfo> GetMyPhotosInfo(int myId);

        Photo GetPhoto(Guid id);

        Guid GetProfilePicGuid(int myId);
    }
}