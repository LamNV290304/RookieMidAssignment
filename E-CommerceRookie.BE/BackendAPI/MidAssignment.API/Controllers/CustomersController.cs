using Microsoft.AspNetCore.Mvc;
using MidAssignment.Application.Usecase;
using MidAssignment.Shared.DTOs;
using System;
using System.Threading.Tasks;

namespace MidAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            try
            {
                var id = await _customerService.CreateCustomerAsync(dto);
                return Ok(id);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the customer.");
            }
        }
    }
}
