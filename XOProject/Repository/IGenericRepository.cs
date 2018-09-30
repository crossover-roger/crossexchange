using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public interface IGenericRepository<TEntity, TKeyType>
    {
        IQueryable<TEntity> Query();

        Task<TEntity> FindByIdAsync(TKeyType id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}