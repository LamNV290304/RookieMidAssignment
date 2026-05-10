using System;
using System.Collections.Generic;
using System.Text;

namespace MidAssignment.Application.Usecase.Interface
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CustomerCreateDto dto);
        Task UpdateCustomerAsync(Guid id, CustomerUpdateDto dto);
        Task DeleteCustomerAsync(Guid id);
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<PagedResultDto<CustomerDto>> GetPagedCustomersAsync(int pageNumber, int pageSize, string? keyword = null);
    }

}
