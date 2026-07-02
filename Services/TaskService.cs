using Models;
using DataAccess.Repositories;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Tasks> _taskRepository;
        public TaskService(IRepository<Tasks> repository)
        {
            _taskRepository = repository;
        }
        public IEnumerable<Tasks> GetTasks(int pageNumber, int pageSize, int? categoryId, string? searchQuery)
        {
            IQueryable<Tasks> query = _taskRepository.GetAll();

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
        public Tasks? GetTaskById(int id)
        {
            return _taskRepository.GetById(id);
        }

        public Tasks CreateTask(Tasks task)
        {
            task.CreatedAt = DateTime.Now;
            task.IsCompleted = false;
            _taskRepository.Add(task);
            _taskRepository.SaveChanges();

            return task;
        }

        public void UpdateTask(Tasks task)
        {
            Tasks? existingTask = _taskRepository.GetById(task.Id);
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

        public void DeleteTask(int id)
        {
            Tasks? task = _taskRepository.GetById(id);
            if (task != null)
            {
                _taskRepository.Delete(task);
                _taskRepository.SaveChanges();
            }
        }
    }
}
