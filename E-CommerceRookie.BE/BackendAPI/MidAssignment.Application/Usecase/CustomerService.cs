using System;
using System.Threading.Tasks;
using FluentValidation;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Application.Usecase
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CustomerCreateDto dto);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IValidator<CustomerCreateDto> _validator;

        public CustomerService(IGenericRepository<Customer> repository, IValidator<CustomerCreateDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Guid> CreateCustomerAsync(CustomerCreateDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await _repository.AddAsync(customer);
            return customer.Id;
        }
    }
}
