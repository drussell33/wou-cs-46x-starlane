using iCollections.Models;

namespace iCollections.Data.Abstract
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Collection GetIcollectionsByUsername(string username);
        bool Exists(IcollectionUser user);

        int GetReadableUserID(string username);

        int GetProfilePicID(int userId);
    }
}