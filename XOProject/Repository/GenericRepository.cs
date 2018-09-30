using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XOProject
{
    public abstract class GenericRepository<TEntity, TKeyType> : IGenericRepository<TEntity, TKeyType>
        where TEntity : BaseModel<TKeyType>, new()
        where TKeyType : IComparable
    {
        protected ExchangeContext _dbContext { get; set; }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> FindByIdAsync(TKeyType id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }     

        public async Task InsertAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}