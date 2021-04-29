using iCollections.Models;
using System.Collections.Generic;

namespace iCollections.Data.Abstract
{
    public interface ICollectionKeywordRepository : IRepository<CollectionKeyword>
    {
        List<CollectionKeyword> GetCollectionKeywordsByUser(IcollectionUser user);

        List<CollectionKeyword> GetCollectionKeywordsByUserSortedAscending(IcollectionUser user, string sort);

        List<CollectionKeyword> GetUserCollectionKeywordsByKeyword(IcollectionUser user, string keyword);
    }
}