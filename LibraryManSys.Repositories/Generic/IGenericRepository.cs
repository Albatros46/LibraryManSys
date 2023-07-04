using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LibraryManSys.Repositories.Generic
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true);
        T GetById(object id);
        T GetByIdAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRange(List<T> entity);

        void Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(T entity);
        Task<T> DeleteAsync(T entity);
        void DeleteRange(List<T> entity);

        bool Exists(Expression<Func<T, bool>> filter);
    }
}