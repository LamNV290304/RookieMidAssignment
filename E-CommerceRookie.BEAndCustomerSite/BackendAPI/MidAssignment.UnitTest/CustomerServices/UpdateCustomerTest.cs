using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerServices
{
    public class UpdateCustomerTest
    {
        [Fact]
        public async Task UpdateCustomerAsync_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var repository = new FakeCustomerRepository(new[]
            {
                new Customer { Id = Guid.NewGuid(), Name = "Old", Email = "old@example.com", Phone = "000" }
            });
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            updateValidator.RuleFor(x => x.Email).NotEmpty();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerUpdateDto
            {
                Name = "New",
                Email = string.Empty,
                Phone = "111"
            };

            // Act
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdateCustomerAsync(repository.Items[0].Id, dto));

            // Assert
            Assert.Empty(repository.UpdatedEntities);
        }

        [Fact]
        public async Task UpdateCustomerAsync_WhenNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repository = new FakeCustomerRepository();
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            updateValidator.RuleFor(x => x.Email).NotEmpty();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerUpdateDto
            {
                Name = "New",
                Email = "new@example.com",
                Phone = "111"
            };

            // Act + Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.UpdateCustomerAsync(Guid.NewGuid(), dto));
        }

        [Fact]
        public async Task UpdateCustomerAsync_WhenValid_UpdatesCustomer()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = new FakeCustomerRepository(new[]
            {
                new Customer { Id = id, Name = "Old", Email = "old@example.com", Phone = "000" }
            });
            var createValidator = new InlineValidator<CustomerCreateDto>();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            updateValidator.RuleFor(x => x.Email).NotEmpty();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerUpdateDto
            {
                Name = "New",
                Email = "new@example.com",
                Phone = "111"
            };

            // Act
            await service.UpdateCustomerAsync(id, dto);

            // Assert
            var updated = Assert.Single(repository.UpdatedEntities);
            Assert.Equal(id, updated.Id);
            Assert.Equal(dto.Name, updated.Name);
            Assert.Equal(dto.Email, updated.Email);
            Assert.Equal(dto.Phone, updated.Phone);
        }
    }
}
