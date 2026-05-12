using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.Domain.Exceptions;

namespace MidAssignment.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, "Validation failed.", ex.Errors);
            }
            catch (KeyNotFoundException ex)
            {
                await WriteProblemDetailsAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConflictException ex)
            {
                await WriteProblemDetailsAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static async Task WriteProblemDetailsAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string title,
            IEnumerable<FluentValidation.Results.ValidationFailure>? errors = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = title
            };

            if (errors != null)
            {
                problemDetails.Extensions["errors"] = errors;
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
