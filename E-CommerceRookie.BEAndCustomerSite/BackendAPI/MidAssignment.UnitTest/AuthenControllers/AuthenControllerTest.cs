using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.API.Controllers;
using MidAssignment.Application.Usecase.Interface;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.AuthenControllers
{
    public class AuthenControllerTest
    {
        [Fact]
        public async Task Login_WhenValid_ReturnsOkWithToken()
        {
            // Arrange
            var service = new FakeAuthenService { TokenToReturn = "token" };
            var controller = new AuthenController(service);
            var dto = new LoginDto { Username = "admin@example.com", Password = "Password123" };

            // Act
            var result = await controller.Login(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var payload = ok.Value?.GetType().GetProperty("Token")?.GetValue(ok.Value)?.ToString();
            Assert.Equal("token", payload);
            Assert.Equal(dto, service.LastLoginDto);
        }

        [Fact]
        public async Task Login_WhenValidationFails_ReturnsBadRequest()
        {
            // Arrange
            var service = new FakeAuthenService { ExceptionToThrow = new ValidationException("Invalid") };
            var controller = new AuthenController(service);
            var dto = new LoginDto { Username = "", Password = "" };

            // Act
            var result = await controller.Login(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_WhenUnauthorized_ReturnsUnauthorized()
        {
            // Arrange
            var service = new FakeAuthenService { ExceptionToThrow = new UnauthorizedAccessException() };
            var controller = new AuthenController(service);
            var dto = new LoginDto { Username = "admin@example.com", Password = "bad" };

            // Act
            var result = await controller.Login(dto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        private sealed class FakeAuthenService : IAuthenService
        {
            public LoginDto? LastLoginDto { get; private set; }
            public string TokenToReturn { get; set; } = "";
            public Exception? ExceptionToThrow { get; set; }

            public Task<string> Login(LoginDto loginDto)
            {
                LastLoginDto = loginDto;
                if (ExceptionToThrow != null)
                {
                    throw ExceptionToThrow;
                }

                return Task.FromResult(TokenToReturn);
            }
        }
    }
}
