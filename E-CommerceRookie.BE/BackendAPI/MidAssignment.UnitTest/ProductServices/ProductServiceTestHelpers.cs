using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;

namespace MidAssignment.UnitTest.ProductServices
{
    internal sealed class FakeProductRepository : IGenericRepository<Product>
    {
        private readonly List<Product> _items;

        public FakeProductRepository(IEnumerable<Product>? seed = null)
        {
            _items = seed?.ToList() ?? new List<Product>();
        }

        public IReadOnlyList<Product> Items => _items;
        public List<Product> AddedEntities { get; } = new();
        public List<Product> UpdatedEntities { get; } = new();
        public List<Product> DeletedEntities { get; } = new();

        public Task<Product?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_items.FirstOrDefault(p => p.Id == id));
        }

        public Task<Product?> GetByIdWithIncludeAsync(Guid id, params string[] includes)
        {
            return Task.FromResult(_items.FirstOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_items.ToList());
        }

        public Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Product, bool>>? predicate)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            params string[] includes)
        {
            return Task.FromResult(PageItems(_items, pageNumber, pageSize));
        }

        public Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Product, bool>>? predicate,
            params string[] includes)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();

            return Task.FromResult(PageItems(filtered, pageNumber, pageSize));
        }

        public Task AddAsync(Product entity)
        {
            AddedEntities.Add(entity);
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product entity)
        {
            UpdatedEntities.Add(entity);

            var index = _items.FindIndex(p => p.Id == entity.Id);
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

        public Task DeleteAsync(Product entity)
        {
            DeletedEntities.Add(entity);
            _items.RemoveAll(p => p.Id == entity.Id);
            return Task.CompletedTask;
        }

        private static (IEnumerable<Product> Items, int TotalCount) PageItems(
            IReadOnlyCollection<Product> items,
            int pageNumber,
            int pageSize)
        {
            var totalCount = items.Count;
            var skip = Math.Max(0, (pageNumber - 1) * pageSize);
            var pageItems = items.Skip(skip).Take(pageSize).ToList();
            return (pageItems, totalCount);
        }
    }

    internal sealed class FakeCategoryRepository : IGenericRepository<Category>
    {
        private readonly List<Category> _items;

        public FakeCategoryRepository(IEnumerable<Category>? seed = null)
        {
            _items = seed?.ToList() ?? new List<Category>();
        }

        public IReadOnlyList<Category> Items => _items;

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
            return Task.FromResult(((IEnumerable<Category>)_items.ToList(), _items.Count));
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<Category, bool>>? predicate)
        {
            var filtered = predicate == null
                ? _items
                : _items.Where(predicate.Compile()).ToList();
            return Task.FromResult(((IEnumerable<Category>)filtered, filtered.Count));
        }

        public Task<(IEnumerable<Category> Items, int TotalCount)> GetPagedWithIncludeAsync(
            int pageNumber,
            int pageSize,
            params string[] includes)
        {
            return Task.FromResult(((IEnumerable<Category>)_items.ToList(), _items.Count));
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
            return Task.FromResult(((IEnumerable<Category>)filtered, filtered.Count));
        }

        public Task AddAsync(Category entity)
        {
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Category entity)
        {
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
            _items.RemoveAll(c => c.Id == entity.Id);
            return Task.CompletedTask;
        }
    }

    internal sealed class FakeWebHostEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "UnitTest";
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public string WebRootPath { get; set; } = string.Empty;
        public string EnvironmentName { get; set; } = "Development";
        public string ContentRootPath { get; set; } = string.Empty;
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }

    internal sealed class TempContentRoot : IDisposable
    {
        public string RootPath { get; }
        public string ContentRootPath { get; }

        private TempContentRoot(string rootPath, string contentRootPath)
        {
            RootPath = rootPath;
            ContentRootPath = contentRootPath;
        }

        public static TempContentRoot Create()
        {
            var root = Path.Combine(Path.GetTempPath(), "MidAssignmentTests", Guid.NewGuid().ToString("N"));
            var appPath = Path.Combine(root, "App");
            Directory.CreateDirectory(appPath);
            return new TempContentRoot(root, appPath);
        }

        public void Dispose()
        {
            if (Directory.Exists(RootPath))
            {
                Directory.Delete(RootPath, true);
            }
        }
    }

    internal static class ProductTestFileHelper
    {
        public static IFormFile CreateFormFile(string fileName, byte[] content)
        {
            var stream = new MemoryStream(content);
            return new FormFile(stream, 0, content.Length, "file", fileName);
        }

        public static string GetUploadsPath(string contentRootPath)
        {
            return Path.GetFullPath(
                Path.Combine(contentRootPath, "..", "MidAssignment.Shared", "Uploads"));
        }
    }
}
