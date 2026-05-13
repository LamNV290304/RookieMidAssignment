using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;

namespace MidAssignment.UnitTest.CustomerServices
{
    internal sealed class FakeCustomerRepository : IGenericRepository<Customer>
    {
        private readonly List<Customer> _items;

        public FakeCustomerRepository(IEnumerable<Customer>? seed = null)
        {
            _items = seed?.ToList() ?? new List<Customer>();
        }

        public IReadOnlyList<Customer> Items => _items;
        public List<Customer> AddedEntities { get; } = new();
        public List<Customer> UpdatedEntities { get; } = new();
        public List<Customer> DeletedEntities { get; } = new();

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_items.FirstOrDefault(c => c.Id == id));
        }

        public Task<Customer?> GetByIdWithIncludeAsync(Guid id, params string[] includes)
        {
            return Task.FromResult(_items.FirstOrDefault(c => c.Id == id));
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Customer>>(_items.ToList());
        }

        public Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Customer, bool>>? predicate)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            params string[] includes)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Customer, bool>>? predicate,
            params string[] includes)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task AddAsync(Customer entity)
        {
            AddedEntities.Add(entity);
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Customer entity)
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

        public Task DeleteAsync(Customer entity)
        {
            DeletedEntities.Add(entity);
            _items.RemoveAll(c => c.Id == entity.Id);
            return Task.CompletedTask;
        }

        private static (IEnumerable<Customer> Items, int TotalCount) PageItems(
            IReadOnlyCollection<Customer> items,
            int pageNumber,
            int pageSize)
        {
            var totalCount = items.Count;
            var skip = Math.Max(0, (pageNumber - 1) * pageSize);
            var pageItems = items.Skip(skip).Take(pageSize).ToList();
            return (pageItems, totalCount);
        }
    }

    internal sealed class FakeEmailCustomerRepository : ICustomerRepository
    {
        public bool EmailExists { get; set; }

        public Task<bool> IsEmailExist(string email)
        {
            return Task.FromResult(EmailExists);
        }
    }
}
