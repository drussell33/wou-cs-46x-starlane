using iCollections.Models;

namespace iCollections.Data.Abstract
{
    public interface IIcollectionUserRepository : IRepository<IcollectionUser>
    {
        IcollectionUser GetIcollectionUserByIdentityId(string identityID);
        bool Exists(IcollectionUser user);

        int GetReadableUserID(string username);

        int GetProfilePicID(int userId);
    }
}