using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using MidAssignment.Application.Usecase;
using MidAssignment.Domain.Entities;
using MidAssignment.Domain.Interfaces;
using MidAssignment.Shared.DTOs;

namespace MidAssignment.UnitTest.AuthenServices
{
    public class AuthenServiceTest
    {
        [Fact]
        public async Task Login_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var repository = new FakeAdminRepository();
            var validator = new InlineValidator<LoginDto>();
            validator.RuleFor(x => x.Username).NotEmpty();
            validator.RuleFor(x => x.Password).NotEmpty();
            var service = new AuthenService(repository, BuildConfiguration(), validator);

            var dto = new LoginDto { Username = string.Empty, Password = string.Empty };

            // Act + Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.Login(dto));
        }

        [Fact]
        public async Task Login_WhenAdminNotFound_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var repository = new FakeAdminRepository { AdminToReturn = null };
            var validator = BuildValidator();
            var service = new AuthenService(repository, BuildConfiguration(), validator);

            var dto = new LoginDto { Username = "admin@example.com", Password = "Password123" };

            // Act + Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.Login(dto));
        }

        [Fact]
        public async Task Login_WhenPasswordInvalid_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var admin = new AdminUser
            {
                Id = Guid.NewGuid(),
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword")
            };
            var repository = new FakeAdminRepository { AdminToReturn = admin };
            var validator = BuildValidator();
            var service = new AuthenService(repository, BuildConfiguration(), validator);

            var dto = new LoginDto { Username = admin.Email, Password = "WrongPassword" };

            // Act + Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.Login(dto));
        }

        [Fact]
        public async Task Login_WhenValid_ReturnsJwtWithExpectedClaims()
        {
            // Arrange
            var admin = new AdminUser
            {
                Id = Guid.NewGuid(),
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword")
            };
            var repository = new FakeAdminRepository { AdminToReturn = admin };
            var validator = BuildValidator();
            var configuration = BuildConfiguration();
            var service = new AuthenService(repository, configuration, validator);

            var dto = new LoginDto { Username = admin.Email, Password = "CorrectPassword" };

            // Act
            var token = await service.Login(dto);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Assert.Equal("TestIssuer", jwt.Issuer);
            Assert.Contains("TestAudience", jwt.Audiences);
            Assert.Equal(admin.Id.ToString(), GetClaim(jwt, JwtRegisteredClaimNames.Sub));
            Assert.Equal(admin.Email, GetClaim(jwt, JwtRegisteredClaimNames.UniqueName));
            Assert.Equal("Admin", GetClaim(jwt, ClaimTypes.Role));
        }

        private static string? GetClaim(JwtSecurityToken jwt, string type)
        {
            return jwt.Claims.FirstOrDefault(c => c.Type == type)?.Value;
        }

        private static InlineValidator<LoginDto> BuildValidator()
        {
            var validator = new InlineValidator<LoginDto>();
            validator.RuleFor(x => x.Username).NotEmpty();
            validator.RuleFor(x => x.Password).NotEmpty();
            return validator;
        }

        private static IConfiguration BuildConfiguration()
        {
            var settings = new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "Qk9xZk5zR2VYb2J3a0x5dE9VdXh6d3FZQ1p4aGJ5R1pZVnN2U0xFQk1aR1d3R2xHc2R6Q2J2d1h5b2tUQm9XcA==",
                ["Jwt:Issuer"] = "TestIssuer",
                ["Jwt:Audience"] = "TestAudience",
                ["Jwt:DurationInMinutes"] = "60"
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }

        private sealed class FakeAdminRepository : IAdminRepository
        {
            public AdminUser? AdminToReturn { get; set; }
            public string? LastEmail { get; private set; }

            public Task<AdminUser?> GetAdminByEmailAsync(string email)
            {
                LastEmail = email;
                return Task.FromResult(AdminToReturn);
            }
        }
    }
}
