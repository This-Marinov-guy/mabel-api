using Microsoft.AspNetCore.Mvc;
using MabelApi.Models;
using MabelApi.Services;

namespace MabelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ISupabaseService _supabaseService;
    private readonly ILogger<TodoController> _logger;

    public TodoController(ISupabaseService supabaseService, ILogger<TodoController> logger)
    {
        _supabaseService = supabaseService;
        _logger = logger;
    }

    // GET: api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos()
    {
        try
        {
            var client = _supabaseService.GetClient();
            var response = await client.From<TodoItem>().Get();
            return Ok(response.Models);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching todos");
            return StatusCode(500, new { message = "Error fetching todos", error = ex.Message });
        }
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoById(int id)
    {
        try
        {
            var client = _supabaseService.GetClient();
            var response = await client.From<TodoItem>()
                .Where(x => x.Id == id)
                .Single();

            if (response == null)
                return NotFound(new { message = $"Todo with id {id} not found" });

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching todo with id {Id}", id);
            return StatusCode(500, new { message = "Error fetching todo", error = ex.Message });
        }
    }

    // POST: api/todo
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] TodoItem todo)
    {
        try
        {
            var client = _supabaseService.GetClient();
            todo.CreatedAt = DateTime.UtcNow;
            todo.UpdatedAt = DateTime.UtcNow;

            var response = await client.From<TodoItem>().Insert(todo);
            var createdTodo = response.Models.FirstOrDefault();

            if (createdTodo == null)
                return BadRequest(new { message = "Failed to create todo" });

            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating todo");
            return StatusCode(500, new { message = "Error creating todo", error = ex.Message });
        }
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItem>> UpdateTodo(int id, [FromBody] TodoItem todo)
    {
        try
        {
            var client = _supabaseService.GetClient();
            todo.Id = id;
            todo.UpdatedAt = DateTime.UtcNow;

            var response = await client.From<TodoItem>().Update(todo);
            var updatedTodo = response.Models.FirstOrDefault();

            if (updatedTodo == null)
                return NotFound(new { message = $"Todo with id {id} not found" });

            return Ok(updatedTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating todo with id {Id}", id);
            return StatusCode(500, new { message = "Error updating todo", error = ex.Message });
        }
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo(int id)
    {
        try
        {
            var client = _supabaseService.GetClient();
            await client.From<TodoItem>()
                .Where(x => x.Id == id)
                .Delete();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting todo with id {Id}", id);
            return StatusCode(500, new { message = "Error deleting todo", error = ex.Message });
        }
    }
}

