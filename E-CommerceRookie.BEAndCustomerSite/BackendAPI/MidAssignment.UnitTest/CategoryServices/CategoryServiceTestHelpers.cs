using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;

namespace MidAssignment.UnitTest.CategoryServices
{
    internal sealed class FakeCategoryRepository : IGenericRepository<Category>
    {
        private readonly List<Category> _items;

        public FakeCategoryRepository(IEnumerable<Category>? seed = null)
        {
            _items = seed?.ToList() ?? new List<Category>();
        }

        public IReadOnlyList<Category> Items => _items;
        public List<Category> AddedEntities { get; } = new();
        public List<Category> UpdatedEntities { get; } = new();
        public List<Category> DeletedEntities { get; } = new();

        public Task<Category?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_items.FirstOrDefault(c => c.Id == id));
        }

        public Task<Category?> GetByIdWithIncludeAsync(Guid id, params string[] includes)
        {
            return Task.FromResult(_items.FirstOrDefault(c => c.Id == id));
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Category>>(_items.ToList());
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Category, bool>>? predicate)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            params string[] includes)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Category, bool>>? predicate,
            params string[] includes)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task AddAsync(Category entity)
        {
            AddedEntities.Add(entity);
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Category entity)
        {
            UpdatedEntities.Add(entity);

            var index = _items.FindIndex(c => c.Id == entity.Id);
            if (index >= 0)
            {
                _items[index] = entity;
            }
            else
            {
                _items.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Category entity)
        {
            DeletedEntities.Add(entity);
            _items.RemoveAll(c => c.Id == entity.Id);
            return Task.CompletedTask;
        }

        private static (IEnumerable<Category> Items, int TotalCount) PageItems(
            IReadOnlyCollection<Category> items,
            int pageNumber,
            int pageSize)
        {
            var totalCount = items.Count;
            var skip = Math.Max(0, (pageNumber - 1) * pageSize);
            var pageItems = items.Skip(skip).Take(pageSize).ToList();
            return (pageItems, totalCount);
        }
    }
}
