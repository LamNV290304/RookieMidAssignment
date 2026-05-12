using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.API.Controllers;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.ProductControllers
{
    public class ProductsControllerTest
    {
        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var service = new FakeProductService
            {
                PagedResultToReturn = new PagedResultDto<ProductDto>
                {
                    Items = new List<ProductDto>
                    {
                        new ProductDto { Id = Guid.NewGuid(), Name = "Mouse", Price = 25m }
                    },
                    TotalCount = 1,
                    PageNumber = 1,
                    PageSize = 10
                }
            };
            var controller = new ProductsController(service);

            // Act
            var result = await controller.GetAll(1, 10, "Mouse", categoryId);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<PagedResultDto<ProductDto>>(ok.Value);
            Assert.Equal(1, payload.TotalCount);
            Assert.Equal((1, 10, "Mouse", categoryId), service.LastGetPaged);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeProductService
            {
                ProductToReturn = new ProductDto { Id = id, Name = "Mouse" }
            };
            var controller = new ProductsController(service);

            // Act
            var result = await controller.GetById(id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<ProductDto>(ok.Value);
            Assert.Equal(id, payload.Id);
            Assert.Equal(id, service.LastGetById);
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreatedAtAction()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeProductService { CreateResult = id };
            var controller = new ProductsController(service);
            var dto = new ProductCreateDto { Name = "Mouse", Description = "Wireless", Price = 25m };

            // Act
            var result = await controller.Create(dto);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Create", created.ActionName);
            Assert.Equal(id, created.RouteValues?["id"]);
            Assert.Equal(id, created.Value);
            Assert.Equal(dto, service.LastCreateDto);
        }

        [Fact]
        public async Task Update_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeProductService();
            var controller = new ProductsController(service);
            var dto = new ProductUpdateDto { Name = "Mouse", Description = "Updated", Price = 30m };

            // Act
            var result = await controller.Update(id, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal((id, dto), service.LastUpdate);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeProductService();
            var controller = new ProductsController(service);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(id, service.LastDeleteId);
        }

        [Fact]
        public async Task DeleteImage_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeProductService();
            var controller = new ProductsController(service);
            var url = "/uploads/a.png";

            // Act
            var result = await controller.DeleteImage(id, url);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal((id, url), service.LastDeleteImage);
        }
    }
}
