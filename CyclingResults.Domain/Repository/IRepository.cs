using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Domain.Repository
{
    /// <summary>
    /// Basic interface that assumes the standard (an object using an int for the Id property).
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<int, TEntity>
    {
        //bool Delete(T entityInstance);

        //bool Update(T entityInstance);
    }

    public interface IRepository<TId, TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(TId id);

        Task<TEntity> Add(TEntity entityInstance);

        Task<bool> Update(TEntity entityInstance);

        Task<bool> Delete(TId id);

        Task<bool> Delete(TEntity entityInstance);
    }
}
