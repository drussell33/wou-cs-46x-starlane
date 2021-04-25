using iCollections.Models;

namespace iCollections.Data.Abstract
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Collection GetIcollectionsByUsername(string username);
    }
}