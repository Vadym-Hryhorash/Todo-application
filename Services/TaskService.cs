using TodoApp.Models;
using TodoApp.DataAccess.Repositories;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Tasks> _taskRepository;
        public TaskService(IRepository<Tasks> repository)
        {
            _taskRepository = repository;
        }
        public IEnumerable<Tasks> GetTasks(int pageNumber, int pageSize, int? categoryId, string? searchQuery, int userId)
        {
            IQueryable<Tasks> query = _taskRepository.GetAll().Where(t => t.UserId == userId);

            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(t => t.Title.ToLower().Contains(searchQuery.ToLower()));
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return query.ToList();
        }
        public Tasks? GetTaskById(int id, int userId)
        {
            return _taskRepository.GetAll().FirstOrDefault(t => t.Id == id && t.UserId == userId);
        }

        public Tasks CreateTask(Tasks task)
        {
            task.CreatedAt = DateTime.Now;
            task.IsCompleted = false;
            _taskRepository.Add(task);
            _taskRepository.SaveChanges();

            return task;
        }

        public void UpdateTask(Tasks task, int userId)
        {
            Tasks? existingTask = GetTaskById(task.Id, userId);
            if (existingTask == null)
            {
                throw new Exception("Task not found!");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.CategoryId = task.CategoryId;

            _taskRepository.Update(existingTask);
            _taskRepository.SaveChanges();
        }

        public void DeleteTask(int id, int userId)
        {
            Tasks? task = GetTaskById(id, userId);
            if (task != null)
            {
                _taskRepository.Delete(task);
                _taskRepository.SaveChanges();
            }
        }
    }
}
