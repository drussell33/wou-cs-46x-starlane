using iCollections.Models;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Data.Abstract
{
    public interface IIcollectionUserRepository : IRepository<IcollectionUser>
    {
        IcollectionUser GetIcollectionUserByIdentityId(string identityID);
        IcollectionUser GetUserById(int id);
        bool Exists(IcollectionUser user);

        bool Exists(string UserName);

        int GetReadableUserID(string username);

        int GetProfilePicID(int userId);

        IcollectionUser GetIcollectionUserByUsername(string username);

        IcollectionUser GetSessionUser(string sessionUserId);

        IcollectionUser GetTargetUser(string name);

        int GetReadableID(string username);
    }
}