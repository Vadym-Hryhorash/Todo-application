using TodoApp.Models;

namespace TodoApp.Services.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<Tasks> GetTasks(int pageNumber, int pageSize, int? categoryId, string? searchQuery, int userId);
        Tasks? GetTaskById(int id, int userId);
        Tasks CreateTask(Tasks task);
        void UpdateTask(Tasks task, int userId);
        void DeleteTask(int id, int userId);
    }
}
