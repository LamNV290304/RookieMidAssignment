using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace MidAssignment.Application.Usecase
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IValidator<CustomerCreateDto> _createValidator;
        private readonly IValidator<CustomerUpdateDto> _updateValidator;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            IGenericRepository<Customer> repository, 
            IValidator<CustomerCreateDto> createValidator,
            IValidator<CustomerUpdateDto> updateValidator,
            ICustomerRepository customerRepository)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _customerRepository = customerRepository;
        }

        public async Task<Guid> CreateCustomerAsync(CustomerCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var isEmailUnique = await _customerRepository.IsEmailExist(dto.Email); 
            if (isEmailUnique)
            {
                throw new ConflictException("Email already exists.");
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
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
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;

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

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        public async Task<PagedResultDto<CustomerDto>> GetPagedCustomersAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            keyword = keyword?.Trim();

            Expression<Func<Customer, bool>>? predicate = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var searchTerm = keyword.ToLower();
                predicate = customer =>
                    customer.Name.ToLower().Contains(searchTerm) ||
                    customer.Email.ToLower().Contains(searchTerm) ||
                    customer.Phone.ToLower().Contains(searchTerm);
            }

            var (items, totalCount) = await _repository.GetPagedAsync(pageNumber, pageSize, predicate);

            var customerDtos = items.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
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
