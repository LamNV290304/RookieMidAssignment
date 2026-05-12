using System;
using System.Threading.Tasks;
using MidAssignment.Application.Usecase.Interface;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerControllers
{
    internal sealed class FakeCustomerService : ICustomerService
    {
        public Guid CreateResult { get; set; } = Guid.NewGuid();
        public CustomerCreateDto? LastCreateDto { get; private set; }
        public (Guid Id, CustomerUpdateDto Dto)? LastUpdate { get; private set; }
        public Guid? LastDeleteId { get; private set; }
        public Guid? LastGetById { get; private set; }
        public (int PageNumber, int PageSize, string? Keyword)? LastGetPaged { get; private set; }

        public CustomerDto CustomerToReturn { get; set; } = new CustomerDto();
        public PagedResultDto<CustomerDto> PagedResultToReturn { get; set; } = new PagedResultDto<CustomerDto>();

        public Task<Guid> CreateCustomerAsync(CustomerCreateDto dto)
        {
            LastCreateDto = dto;
            return Task.FromResult(CreateResult);
        }

        public Task UpdateCustomerAsync(Guid id, CustomerUpdateDto dto)
        {
            LastUpdate = (id, dto);
            return Task.CompletedTask;
        }

        public Task DeleteCustomerAsync(Guid id)
        {
            LastDeleteId = id;
            return Task.CompletedTask;
        }

        public Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            LastGetById = id;
            return Task.FromResult(CustomerToReturn);
        }

        public Task<PagedResultDto<CustomerDto>> GetPagedCustomersAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            LastGetPaged = (pageNumber, pageSize, keyword);
            return Task.FromResult(PagedResultToReturn);
        }
    }
}
