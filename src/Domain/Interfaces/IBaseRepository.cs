using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<PaginatedList<T>> GetPaginatedAsync(int pageIndex, int pageSize = 5, Expression<Func<T, object>>? orderBy = null,
        Expression<Func<T, bool>>? filter = null);
        

    }
}
