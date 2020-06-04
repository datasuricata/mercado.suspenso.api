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
        IQueryable<T> Queryable(bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task AtualizarAsync(T entity);

        bool Existe(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        Task<T> PorIdAsync(string id, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> PorAsync(Expression<Func<T, bool>> where, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task InserirAsync(T entity);

        Task InserirRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> ListarAsync(bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> ListarPorAsync(Expression<Func<T, bool>> where, bool noTracking = true, params Expression<Func<T, object>>[] includes);
    }
}