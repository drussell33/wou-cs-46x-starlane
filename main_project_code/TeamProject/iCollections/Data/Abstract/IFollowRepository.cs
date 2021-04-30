using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCollections.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace iCollections.Data.Abstract
{
    public interface IFollowRepository : IRepository<Follow>
    {
        bool Exists(Follow follow);

        IIncludableQueryable<Follow, IcollectionUser> GetFollows();

        Follow GetFollow(Func<Follow, bool> filter);

        List<Follow> GetFolloweesForUserExcludingMe(int userId, int myId);
    }
}
