using iCollections.Models;

namespace iCollections.Data.Abstract
{
    public interface IIcollectionUserRepository : IRepository<IcollectionUser>
    {
        IcollectionUser GetIcollectionUserByIdentityId(string identityID);
        bool Exists(IcollectionUser user);

        bool Exists(string UserName);

        int GetReadableUserID(string username);

        int GetProfilePicID(int userId);

        IcollectionUser GetSessionUser(string sessionUserId);

        IcollectionUser GetTargetUser(string name);

        int GetReadableID(string username);
    }
}