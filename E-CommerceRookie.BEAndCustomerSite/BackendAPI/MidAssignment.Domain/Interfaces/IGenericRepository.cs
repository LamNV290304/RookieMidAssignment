using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MidAssignment.Domain.Entities;

namespace MidAssignment.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdWithIncludeAsync(Guid id, params string[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedWithIncludeAsync(int pageNumber, int pageSize, params string[] includes);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedWithIncludeAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate, params string[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
