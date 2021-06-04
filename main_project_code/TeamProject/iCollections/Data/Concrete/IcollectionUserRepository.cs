using iCollections.Data.Abstract;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
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

        public virtual bool Exists(string UserName)
        {
            return _dbSet.Any(x => x.UserName == UserName);
        }

        public virtual IcollectionUser GetIcollectionUserByIdentityId(string identityID)
        {
            return _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefault();
        }

        public virtual IcollectionUser GetUserById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public int GetReadableUserID(string nastyString)
        {
            return _dbSet.FirstOrDefault(u => u.AspnetIdentityId == nastyString).Id;
        }

        public int GetReadableID(string username)
        {
            return _dbSet.FirstOrDefault(u => u.UserName == username).Id;
        }

        public int GetProfilePicID(int userId)
        {
            return _dbSet.FirstOrDefault(u => u.Id == userId).ProfilePicId ?? 0;
        }

        public IcollectionUser GetSessionUser(string sessionUserId)
        {
            return _dbSet.Include(u => u.FollowFollowerNavigations).Include(u => u.FollowFollowedNavigations).FirstOrDefault(m => m.AspnetIdentityId == sessionUserId);
        }

        //This is whats slowing down the userpage load. it never used the photos so I commented that out and now it seems chill.
        public IcollectionUser GetTargetUser(string name)
        {
            return _dbSet
                //.Include(u => u.Photos)
                .Include(u => u.FollowFollowerNavigations)
                .Include(u => u.FollowFollowedNavigations)
                .ThenInclude(f => f.FollowerNavigation)
                .FirstOrDefault(m => m.UserName == name);
        }

        public virtual IcollectionUser GetIcollectionUserByUsername(string username)
        {
            return _dbSet.FirstOrDefault(m => m.UserName == username);
        }

    }
}