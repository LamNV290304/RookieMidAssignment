using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerServices
{
    public class GetPagedCustomersTest
    {
        [Fact]
        public async Task GetPagedCustomersAsync_WhenNoKeyword_ReturnsCorrectPage()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "A", Email = "a@example.com", Phone = "111" },
                new Customer { Id = Guid.NewGuid(), Name = "B", Email = "b@example.com", Phone = "222" },
                new Customer { Id = Guid.NewGuid(), Name = "C", Email = "c@example.com", Phone = "333" },
                new Customer { Id = Guid.NewGuid(), Name = "D", Email = "d@example.com", Phone = "444" }
            };

            var repository = new FakeCustomerRepository(customers);
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            // Act
            var result = await service.GetPagedCustomersAsync(2, 2);
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(4, result.TotalCount);
            Assert.Equal(2, result.PageNumber);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(2, items.Count);
            Assert.Equal("C", items[0].Name);
            Assert.Equal("D", items[1].Name);
        }

        [Fact]
        public async Task GetPagedCustomersAsync_WhenKeywordProvided_FiltersItems()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com", Phone = "111" },
                new Customer { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@example.com", Phone = "222" },
                new Customer { Id = Guid.NewGuid(), Name = "Alicia", Email = "alicia@example.com", Phone = "333" }
            };

            var repository = new FakeCustomerRepository(customers);
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            // Act
            var result = await service.GetPagedCustomersAsync(1, 10, "Ali");
            var items = result.Items.ToList();

            // Assert
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, items.Count);
            Assert.Contains(items, item => item.Name == "Alice");
            Assert.Contains(items, item => item.Name == "Alicia");
        }
    }
}
