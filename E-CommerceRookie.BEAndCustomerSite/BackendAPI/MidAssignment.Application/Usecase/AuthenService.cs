using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MidAssignment.Application.Usecase
{
    public class AuthenService : IAuthenService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthenService(IAdminRepository adminRepository, IConfiguration configuration, IValidator<LoginDto> loginValidator)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
            _loginValidator = loginValidator;
        }
        public async Task<string> Login(LoginDto loginDto)
        {
            var validationResult = _loginValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var admin = await _adminRepository.GetAdminByEmailAsync(loginDto.Username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = GenerateJwtToken(admin.Id, admin.Email);
            return token;
        }

        private string GenerateJwtToken(Guid adminId, string email)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, adminId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)
                };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["Jwt:DurationInMinutes"]!)
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
