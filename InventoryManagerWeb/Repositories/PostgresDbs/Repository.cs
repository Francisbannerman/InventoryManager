using System.Linq.Expressions;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class Repository <T> : IRepository<T> where T : class
{
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;
    private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
            // _db.Products.Include(u => u.category)
            //     .Include(u => u.categoryId);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            await Task.CompletedTask;
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _db.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','},
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

       public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.AnyAsync(filter);
        }

       public async Task<int> MaxAsync(Expression<Func<T, int>> entity)
       {
           return await dbSet.MaxAsync(entity);
       }

       public async Task EditAsync(T entity)
       {
           dbSet.Attach(entity);
           _db.Entry(entity).State = EntityState.Modified;
           await Task.CompletedTask;
       }

       public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
       {
           IQueryable<T> query;
           if (tracked)
           {
               query = dbSet;
           }
           else
           {
               query = dbSet.AsNoTracking();
           }
           query = query.Where(filter);
           if (!string.IsNullOrEmpty(includeProperties))
           {
               foreach (var includeProp in includeProperties.Split(new char[]{','},
                            StringSplitOptions.RemoveEmptyEntries))
               {
                   query = query.Include(includeProp);
               }
           }
           return query.FirstOrDefault();
       }
}