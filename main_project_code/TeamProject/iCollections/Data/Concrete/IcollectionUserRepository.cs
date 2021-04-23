using iCollections.Data.Abstract;
using iCollections.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace iCollections.Data.Concrete
{
    public class IcollectionUserRepository : Repository<IcollectionUser>, IIcollectionUserRepository
    {
        public IcollectionUserRepository(ICollectionsDbContext ctx) : base(ctx) { }

        public virtual bool Exists(IcollectionUser user)
        {
            return _dbSet.Any(x => x.AspnetIdentityId == user.AspnetIdentityId
                && x.FirstName == user.FirstName
                && x.LastName == user.LastName);
        }

        public virtual IcollectionUser GetIcollectionUserByIdentityId(string identityID)
        {
            return _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefault();
        }

        public int GetReadableUserID(string username)
        {
            return GetAll().FirstOrDefault(u => u.UserName == username).Id;
        }

        public int GetProfilePicID(int userId)
        {
            return GetAll().FirstOrDefault(u => u.Id == userId).ProfilePicId ?? -1;
        }

        public IcollectionUser GetSessionUser(string sessionUserId)
        {
            return GetAll().Include(u => u.FollowFollowerNavigations).Include(u => u.FollowFollowedNavigations).FirstOrDefault(m => m.AspnetIdentityId == sessionUserId);
        }

        public IcollectionUser GetTargetUser(string name)
        {
            return GetAll()
                .Include(u => u.Photos)
                .Include(u => u.FollowFollowerNavigations)
                .Include(u => u.FollowFollowedNavigations)
                .ThenInclude(f => f.FollowerNavigation)
                .FirstOrDefault(m => m.UserName == name);
        }

    }
}