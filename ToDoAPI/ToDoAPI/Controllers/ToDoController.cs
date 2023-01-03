using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<Models.Task>>> PostTask(Models.Task task)
        {
            if (task == null)
            {
                return BadRequest("task not found");
            }
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Models.Task>>> UpdateTask(int id, Models.Task task)
        {
            var dbTask = await _context.Tasks.FindAsync(id);
            if (dbTask == null)
                return BadRequest("task not found!");
            dbTask.Title = task.Title;
            dbTask.Description = task.Description;
            dbTask.PublishedAt = task.PublishedAt;
            await _context.SaveChangesAsync();
            return Ok(await _context.Tasks.ToListAsync());
        }
        //delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Models.Task>>> DeleteTask(int id)
        {
            var dbTask = await _context.Tasks.FindAsync(id);
            if (dbTask == null)
                return BadRequest("Task not found!");
            _context.Remove(dbTask);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tasks.ToListAsync());
        }
    }


}
