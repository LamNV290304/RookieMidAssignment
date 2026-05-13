using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerServices
{
    public class GetCustomerByIdTest
    {
        [Fact]
        public async Task GetCustomerByIdAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = new FakeCustomerRepository();
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetCustomerByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhenFound_ReturnsDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = new FakeCustomerRepository(new[]
            {
                new Customer { Id = id, Name = "A", Email = "a@example.com", Phone = "123", CreatedAt = DateTime.UtcNow }
            });
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            // Act
            var result = await service.GetCustomerByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal("A", result.Name);
            Assert.Equal("a@example.com", result.Email);
            Assert.Equal("123", result.Phone);
        }
    }
}
