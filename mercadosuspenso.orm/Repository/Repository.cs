using mercadosuspenso.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mercadosuspenso.orm.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly MercadoDbContext context;
        public Repository(MercadoDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> Queryable(bool noTracking, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes.Any())
                query = Include(context.Set<T>(), includes);

            if (noTracking)
                query.AsNoTracking();

            return query;
        }

        public virtual bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includes)
        {
            return includes.Any() ? Include(context.Set<T>(), includes).Any(where) : context.Set<T>().Any(where);
        }

        public virtual async Task<T> ByAsync(Expression<Func<T, bool>> where, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Queryable(noTracking, includes).FirstOrDefaultAsync(where);
        }

        public virtual async Task<T> ByIdAsync(string id, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return includes.Any() ? await Queryable(noTracking, includes).FirstOrDefaultAsync(x => x.Id == id) : await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListByAsync(Expression<Func<T, bool>> where, bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Queryable(noTracking, includes).Where(where).ToListAsync();
        }
        
        public virtual async Task<IEnumerable<T>> ListAsync(bool noTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Queryable(noTracking, includes).ToListAsync();
        }

        public virtual async Task InsertAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity);

            await context.SaveChangesAsync();
        }

        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            foreach (var property in includes)
                query = query.Include(property);

            return query;
        }
    }
}
