using Microsoft.AspNetCore.Mvc;
using MidAssignment.Application.Usecase;
using MidAssignment.Shared.DTOs;
using System;
using System.Threading.Tasks;

namespace MidAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            try
            {
                var id = await _categoryService.CreateCategoryAsync(dto);
                return CreatedAtAction(nameof(Create), new { id }, id);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the category.");
            }
        }
    }
}
