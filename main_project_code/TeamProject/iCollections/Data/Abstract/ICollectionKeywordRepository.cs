using iCollections.Models;
using System.Collections.Generic;

namespace iCollections.Data.Abstract
{
    public interface ICollectionKeywordRepository : IRepository<CollectionKeyword>
    {
        List<CollectionKeyword> GetPublicCollectionKeywordsByUser(IcollectionUser user);

        List<CollectionKeyword> GetPublicAndPrivateCollectionKeywordsByUser(IcollectionUser user);

        List<CollectionKeyword> GetPublicCollectionKeywordsByUserSortedAscending(IcollectionUser user, string sort);

        List<CollectionKeyword> GetUserPublicCollectionKeywordsByKeyword(IcollectionUser user, string keyword);

        List<CollectionKeyword> GetMyFavoriteCollectionsByUser(IcollectionUser user);
    }
}