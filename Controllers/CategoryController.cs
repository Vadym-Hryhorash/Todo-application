using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private int GetCurrentUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User not found!");
            }
            return int.Parse(userIdString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categories>> GetCategories()
        {
            return Ok(_categoryService.GetAllCategories(GetCurrentUserId()));
        }

        [HttpGet("{id}")]
        public ActionResult<Categories> GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id, GetCurrentUserId());
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Categories> CreateCategory([FromBody] Categories category)
        {
            category.UserId = GetCurrentUserId();
            var createdCategory = _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Categories category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                _categoryService.UpdateCategory(category, GetCurrentUserId());
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id, GetCurrentUserId());
            return NoContent();
        }
    }
}