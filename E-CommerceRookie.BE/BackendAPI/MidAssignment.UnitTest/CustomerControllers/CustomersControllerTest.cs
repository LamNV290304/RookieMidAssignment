using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.API.Controllers;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.CustomerControllers
{
    public class CustomersControllerTest
    {
        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var service = new FakeCustomerService
            {
                PagedResultToReturn = new PagedResultDto<CustomerDto>
                {
                    Items = new List<CustomerDto>
                    {
                        new CustomerDto { Id = Guid.NewGuid(), Name = "A", Email = "a@example.com", Phone = "123" }
                    },
                    TotalCount = 1,
                    PageNumber = 1,
                    PageSize = 10
                }
            };
            var controller = new CustomersController(service);

            // Act
            var result = await controller.GetAll(1, 10, "A");

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<PagedResultDto<CustomerDto>>(ok.Value);
            Assert.Equal(1, payload.TotalCount);
            Assert.Equal((1, 10, "A"), service.LastGetPaged);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeCustomerService
            {
                CustomerToReturn = new CustomerDto
                {
                    Id = id,
                    Name = "A",
                    Email = "a@example.com",
                    Phone = "123"
                }
            };
            var controller = new CustomersController(service);

            // Act
            var result = await controller.GetById(id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<CustomerDto>(ok.Value);
            Assert.Equal(id, payload.Id);
            Assert.Equal(id, service.LastGetById);
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeCustomerService { CreateResult = id };
            var controller = new CustomersController(service);
            var dto = new CustomerCreateDto { Name = "A", Email = "a@example.com", Phone = "123" };

            // Act
            var result = await controller.Create(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(id, ok.Value);
            Assert.Equal(dto, service.LastCreateDto);
        }

        [Fact]
        public async Task Update_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new FakeCustomerService();
            var controller = new CustomersController(service);
            var dto = new CustomerUpdateDto { Name = "B", Email = "b@example.com", Phone = "999" };

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
            var service = new FakeCustomerService();
            var controller = new CustomersController(service);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(id, service.LastDeleteId);
        }
    }
}
