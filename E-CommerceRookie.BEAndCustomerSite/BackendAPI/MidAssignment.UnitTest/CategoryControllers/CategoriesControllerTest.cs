using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.API.Controllers;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CategoryControllers
{
    public class CategoriesControllerTest
    {
        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var service = new FakeCategoryService
            {
                PagedResultToReturn = new PagedResultDto<CategoryDto>
                {
                    Items = new List<CategoryDto>
                    {
                        new CategoryDto { Id = Guid.NewGuid(), Name = "A", Description = "Desc" }
                    },
                    TotalCount = 1,
                    PageNumber = 1,
                    PageSize = 5
                }
            };
            var controller = new CategoriesController(service);

            // Act
            var result = await controller.GetAll(1, 5, "A");

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<PagedResultDto<CategoryDto>>(ok.Value);
            Assert.Equal(1, payload.TotalCount);
            Assert.Equal((1, 5, "A"), service.LastGetPaged);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeCategoryService
            {
                CategoryToReturn = new CategoryDto
                {
                    Id = id,
                    Name = "A",
                    Description = "Desc"
                }
            };
            var controller = new CategoriesController(service);

            // Act
            var result = await controller.GetById(id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<CategoryDto>(ok.Value);
            Assert.Equal(id, payload.Id);
            Assert.Equal(id, service.LastGetById);
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreatedAtAction()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeCategoryService { CreateResult = id };
            var controller = new CategoriesController(service);
            var dto = new CategoryCreateDto { Name = "A", Description = "Desc" };

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
            var service = new FakeCategoryService();
            var controller = new CategoriesController(service);
            var dto = new CategoryUpdateDto { Name = "B", Description = "Updated" };

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
            var service = new FakeCategoryService();
            var controller = new CategoriesController(service);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(id, service.LastDeleteId);
        }
    }
}
