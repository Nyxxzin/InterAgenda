using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ui.Services.Generic
{
    public class GenericService<TEntity>
        : IGenericService<TEntity> where TEntity : class
    {

        protected readonly Data.ApplicationDbContext Context;
        protected readonly Microsoft.EntityFrameworkCore.DbSet<TEntity> DbSet;

        public GenericService(Data.ApplicationDbContext context)
        {
            Context = context ?? throw new System.ArgumentNullException($"Argument {nameof(context)} is null");
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity resource)
        {
            DbSet.Add(resource);

            await SaveAsync();

            return resource;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity resource)
        {
            DbSet.Remove(resource);
            await SaveAsync();
            return resource;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity resource, object key)
        {
            if (resource == null)
                return null;

            var exist = await DbSet.FindAsync(key);

            if (exist == null) return null;

            Context.Entry((object)exist).CurrentValues.SetValues(resource);

            await SaveAsync();

            return exist;
        }

        public virtual async Task<int> SaveAsync() => await Context.SaveChangesAsync();

        public virtual Task<IEnumerable<TEntity>> GetAllByUserIdAsync(string id) => throw new System.NotImplementedException();


    }
}
