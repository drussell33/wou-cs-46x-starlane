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
        bool Exists(int id);

        IIncludableQueryable<Follow, IcollectionUser> GetFollows();

        Follow GetFollow(Func<Follow, bool> filter);
        Follow GetFollowLight(Func<Follow, bool> filter);
        Task<Follow> GetFollowAsync(int id);
        List<Follow> GetFolloweesForUserExcludingMe(int userId, int myId);
        List<IcollectionUser> GetMyFollowees(int myId);
    }
}
