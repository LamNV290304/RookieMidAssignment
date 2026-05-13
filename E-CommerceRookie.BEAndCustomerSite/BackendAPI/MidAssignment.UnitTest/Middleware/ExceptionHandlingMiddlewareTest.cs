using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using MidAssignment.API.Middleware;
using MidAssignment.Domain.Exceptions;

namespace MidAssignment.UnitTest.Middleware
{
    public class ExceptionHandlingMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsync_WhenValidationException_ReturnsBadRequestProblemDetails()
        {
            // Arrange
            var failures = new[]
            {
                new ValidationFailure("Name", "Required")
            };
            var exception = new ValidationException(failures);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            Task Next(HttpContext _) => throw exception;
            var middleware = new ExceptionHandlingMiddleware(Next, NullLogger<ExceptionHandlingMiddleware>.Instance);

            // Act
            var (statusCode, json) = await ExecuteAsync(context, middleware);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
            Assert.Equal("Validation failed.", json.RootElement.GetProperty("title").GetString());
            Assert.True(json.RootElement.TryGetProperty("errors", out _));
        }

        [Fact]
        public async Task InvokeAsync_WhenConflictException_ReturnsConflictProblemDetails()
        {
            // Arrange
            var exception = new ConflictException("Email already exists.");
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            Task Next(HttpContext _) => throw exception;
            var middleware = new ExceptionHandlingMiddleware(Next, NullLogger<ExceptionHandlingMiddleware>.Instance);

            // Act
            var (statusCode, json) = await ExecuteAsync(context, middleware);

            // Assert
            Assert.Equal((int)HttpStatusCode.Conflict, statusCode);
            Assert.Equal("Email already exists.", json.RootElement.GetProperty("title").GetString());
        }

        [Fact]
        public async Task InvokeAsync_WhenKeyNotFoundException_ReturnsNotFoundProblemDetails()
        {
            // Arrange
            var exception = new KeyNotFoundException("Customer not found.");
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            Task Next(HttpContext _) => throw exception;
            var middleware = new ExceptionHandlingMiddleware(Next, NullLogger<ExceptionHandlingMiddleware>.Instance);

            // Act
            var (statusCode, json) = await ExecuteAsync(context, middleware);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
            Assert.Equal("Customer not found.", json.RootElement.GetProperty("title").GetString());
        }

        [Fact]
        public async Task InvokeAsync_WhenUnexpectedException_ReturnsInternalServerErrorProblemDetails()
        {
            // Arrange
            var exception = new Exception("Boom");
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            Task Next(HttpContext _) => throw exception;
            var middleware = new ExceptionHandlingMiddleware(Next, NullLogger<ExceptionHandlingMiddleware>.Instance);

            // Act
            var (statusCode, json) = await ExecuteAsync(context, middleware);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
            Assert.Equal("An unexpected error occurred.", json.RootElement.GetProperty("title").GetString());
        }

        private static async Task<(int StatusCode, JsonDocument Json)> ExecuteAsync(
            HttpContext context,
            ExceptionHandlingMiddleware middleware)
        {
            await middleware.InvokeAsync(context);

            context.Response.Body.Position = 0;
            using var reader = new StreamReader(context.Response.Body);
            var payload = await reader.ReadToEndAsync();
            var json = JsonDocument.Parse(payload);
            return (context.Response.StatusCode, json);
        }
    }
}
