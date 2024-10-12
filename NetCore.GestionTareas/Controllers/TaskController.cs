using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.GestionTareas.Models;

namespace NetCore.GestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        List<MTask> tasks = new List<MTask>
                    {
                       new MTask{
                            Id = Guid.NewGuid().ToString(),
                            name = "Task 1",
                            description = "Description 1",
                            dateCreation = DateTime.Now,
                            dateModified = DateTime.Now,
                        },new MTask{
                            Id = Guid.NewGuid().ToString(),
                            name = "Task 2",
                            description = "Description 2",
                            dateCreation = DateTime.Now,
                            dateModified = DateTime.Now,
                        },new MTask{
                            Id = Guid.NewGuid().ToString(),
                            name = "Task 3",
                            description = "Description 3",
                            dateCreation = DateTime.Now,
                            dateModified = DateTime.Now,
                        },new MTask{
                            Id = Guid.NewGuid().ToString(),
                            name = "Task 4",
                            description = "Description 4",
                            dateCreation = DateTime.Now,
                            dateModified = DateTime.Now,
                        },
                    };

        [HttpGet]
        [Route("GetAllTask")]
        public IActionResult GetAllTask()
        {

            return Ok(tasks);

        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(string id)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == id); if (task == null)
                {
                    return BadRequest("Task not found");

                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest("Element found" + ex.Message);
            }

        }


        [HttpPost]
        public IActionResult CreateTask([FromBody] MTask newTask)
        {
            try
            {
                if (string.IsNullOrEmpty(newTask.name))
                {
                    return BadRequest("Task name cannot be empty");
                }
                newTask.Id = Guid.NewGuid().ToString();
                newTask.dateCreation = DateTime.Now;
                newTask.dateModified = DateTime.Now;
                tasks.Add(newTask);
                return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);

            }
            catch (Exception ex)
            {
                return BadRequest("Task Error" + ex.Message);
            }

        }
        [HttpPut("{id}")]
        public IActionResult ModifiTask(string id, [FromBody] MTask taskModified)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return BadRequest("Task not found");

                }
                task.name = taskModified.name;
                task.description = taskModified.description;
                task.dateModified = DateTime.Now;

                return Ok(task);

            }
            catch (Exception ex)
            {
                return BadRequest("Task not found" + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskById(string id)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == id); if (task == null)
                {
                    return BadRequest("Task not found");

                }
                tasks.Remove(task);

                return Ok("Task Deleted");

            }
            catch (Exception ex)
            {
                return BadRequest("Task not found" + ex.Message);
            }

        }
    }
}
