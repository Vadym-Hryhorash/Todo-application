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
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tasks>> GetTasks(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? searchQuery = null)
        {
            int userId = GetCurrentUserId();
            var tasks = _taskService.GetTasks(pageNumber, pageSize, categoryId, searchQuery, userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Tasks> GetTaskById(int id)
        {
            int userId = GetCurrentUserId();

            var task = _taskService.GetTaskById(id, userId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Tasks> CreateTask([FromBody] Tasks task)
        {
            task.UserId = GetCurrentUserId();
            var createdTask = _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            task.UserId = GetCurrentUserId();

            try
            {
                _taskService.UpdateTask(task, GetCurrentUserId());
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _taskService.DeleteTask(id, GetCurrentUserId());
            return NoContent();
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
    }
}
