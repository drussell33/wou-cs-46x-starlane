using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Models;

namespace iCollections.Data.Abstract
{
    public interface IUserRepository : IRepository<IcollectionUser>
    {
        IcollectionUser GetUserByIdentityId(string identityID);
        bool Exists(IcollectionUser user);
        IcollectionUser GetUser(int id);
    }
}
