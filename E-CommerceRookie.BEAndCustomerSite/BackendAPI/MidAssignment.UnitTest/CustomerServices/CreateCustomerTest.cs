using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Exceptions;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerServices
{
    public class CreateCustomerTest
    {
        [Fact]
        public async Task CreateCustomerAsync_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var repository = new FakeCustomerRepository();
            var createValidator = new InlineValidator<CustomerCreateDto>();
            createValidator.RuleFor(x => x.Email).NotEmpty();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerCreateDto
            {
                Name = "A",
                Email = string.Empty,
                Phone = "123"
            };

            // Act + Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateCustomerAsync(dto));
        }

        [Fact]
        public async Task CreateCustomerAsync_WhenEmailExists_ThrowsConflictException()
        {
            // Arrange
            var repository = new FakeCustomerRepository();
            var createValidator = new InlineValidator<CustomerCreateDto>();
            createValidator.RuleFor(x => x.Email).NotEmpty();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = true };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerCreateDto
            {
                Name = "A",
                Email = "a@example.com",
                Phone = "123"
            };

            // Act + Assert
            await Assert.ThrowsAsync<ConflictException>(() => service.CreateCustomerAsync(dto));
        }

        [Fact]
        public async Task CreateCustomerAsync_WhenValid_AddsCustomerAndReturnsId()
        {
            // Arrange
            var repository = new FakeCustomerRepository();
            var createValidator = new InlineValidator<CustomerCreateDto>();
            createValidator.RuleFor(x => x.Email).NotEmpty();
            var updateValidator = new InlineValidator<CustomerUpdateDto>();
            var emailRepository = new FakeEmailCustomerRepository { EmailExists = false };

            var service = new CustomerService(
                repository,
                createValidator,
                updateValidator,
                emailRepository);

            var dto = new CustomerCreateDto
            {
                Name = "A",
                Email = "a@example.com",
                Phone = "123"
            };

            // Act
            var id = await service.CreateCustomerAsync(dto);

            // Assert
            var added = Assert.Single(repository.AddedEntities);
            Assert.NotEqual(Guid.Empty, id);
            Assert.Equal(id, added.Id);
            Assert.Equal(dto.Name, added.Name);
            Assert.Equal(dto.Email, added.Email);
            Assert.Equal(dto.Phone, added.Phone);
        }
    }
}
