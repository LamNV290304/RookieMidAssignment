using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerServices
{
    public class DeleteCustomerTest
    {
        [Fact]
        public async Task DeleteCustomerAsync_WhenNotFound_ThrowsKeyNotFoundException()
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
                service.DeleteCustomerAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteCustomerAsync_WhenFound_RemovesCustomer()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = new FakeCustomerRepository(new[]
            {
                new Customer { Id = id, Name = "A", Email = "a@example.com", Phone = "123" }
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
            await service.DeleteCustomerAsync(id);

            // Assert
            var deleted = Assert.Single(repository.DeletedEntities);
            Assert.Equal(id, deleted.Id);
            Assert.Empty(repository.Items);
        }
    }
}
