using mercadosuspenso.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mercadosuspenso.orm.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Queryable(bool noTracking, params Expression<Func<T, object>>[] includes);

        Task UpdateAsync(T entity);

        bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        Task<T> ByIdAsync(string id, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> ByAsync(Expression<Func<T, bool>> where, bool readOnly = true, params Expression<Func<T, object>>[] includes);

        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> ListAsync(bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> ListByAsync(Expression<Func<T, bool>> where, bool noTracking = true, params Expression<Func<T, object>>[] includes);
    }
}