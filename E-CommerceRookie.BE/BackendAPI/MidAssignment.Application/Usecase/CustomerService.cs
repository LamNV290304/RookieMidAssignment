using System;
using System.Linq;
using System.Threading.Tasks;

namespace MidAssignment.Application.Usecase
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IValidator<CustomerCreateDto> _createValidator;
        private readonly IValidator<CustomerUpdateDto> _updateValidator;

        public CustomerService(
            IGenericRepository<Customer> repository, 
            IValidator<CustomerCreateDto> createValidator,
            IValidator<CustomerUpdateDto> updateValidator)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Guid> CreateCustomerAsync(CustomerCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
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

        public async Task UpdateCustomerAsync(Guid id, CustomerUpdateDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            customer.Name = dto.Name;

            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            await _repository.DeleteAsync(customer);
        }

        public async Task<PagedResultDto<CustomerDto>> GetPagedCustomersAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pageNumber, pageSize);

            var customerDtos = items.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return new PagedResultDto<CustomerDto>
            {
                Items = customerDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
