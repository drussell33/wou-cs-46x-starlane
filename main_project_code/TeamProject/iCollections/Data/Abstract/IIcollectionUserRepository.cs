using iCollections.Models;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Data.Abstract
{
    public interface IIcollectionUserRepository : IRepository<IcollectionUser>
    {
        IcollectionUser GetIcollectionUserByIdentityId(string identityID);
        bool Exists(IcollectionUser user);

        int GetReadableUserID(string username);

        int GetProfilePicID(int userId);

        IcollectionUser GetIcollectionUserByUsername(string username);
    }
}