using System;
using System.Threading.Tasks;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.Application.Usecase
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(Guid id, ProductUpdateDto dto);
    }
}
