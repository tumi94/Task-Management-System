using Microsoft.AspNetCore.Mvc;
using Task_Management_System__Server_.Interfaces;
using Task_Management_System__Server_.Repositories;
using Task = Task_Management_System__Server_.Model.Task;




namespace Task_Management_System__Server_.Controllers
{
    // TaskController.cs
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(TaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetAllTasks()
        {
            try
            {
                var tasks = _taskRepository.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            try
            {
                var task = _taskRepository.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<Task> AddTask([FromBody] Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Task object is null");
                }

                _taskRepository.AddTask(task);
                _unitOfWork.SaveChanges();

                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                // Log the exception
                _unitOfWork.Rollback();
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Task object is null");
                }

                var existingTask = _taskRepository.GetTaskById(id);

                if (existingTask == null)
                {
                    return NotFound();
                }

                // Update only specific properties
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;

                _taskRepository.UpdateTask(existingTask);
                _unitOfWork.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                _unitOfWork.Rollback();
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                _taskRepository.DeleteTask(id);
                _unitOfWork.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                _unitOfWork.Rollback();
                return StatusCode(500, "Internal server error");
            }
        }
    }

}


