using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> TodoItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Name = "Sample Task 1", IsComplete = false },
            new TodoItem { Id = 2, Name = "Sample Task 2", IsComplete = true }
        };

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(TodoItems);
        }

        // GET: api/Todo/{id}
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/Todo
        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem newItem)
        {
            newItem.Id = TodoItems.Count > 0 ? TodoItems.Max(t => t.Id) + 1 : 1;
            TodoItems.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // PUT: api/Todo/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem updatedItem)
        {
            var item = TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;
            item.IsComplete = updatedItem.IsComplete;
            return NoContent();
        }

        // DELETE: api/Todo/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            TodoItems.Remove(item);
            return NoContent();
        }
    }
}