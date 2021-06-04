using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Data.Abstract
{
    /// <summary>
    /// Minimal interface for CRUD operations on entities
    /// </summary>
    /// <typeparam name="TEntity">This is the entity for which we're making a repository</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> FindByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        IQueryable<TEntity> GetAll();
        TEntity AddOrUpdate(TEntity entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(int id);
        // TEntity FindById(int id);
        // void DeleteById(int id);
        // void Delete(TEntity entity);
    }
}