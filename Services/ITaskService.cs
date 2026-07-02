using Models;

namespace Services
{
    public interface ITaskService
    {
        IEnumerable<Tasks> GetTasks(int pageNumber, int pageSize, int? categoryId, string? searchQuery);
        Tasks? GetTaskById(int id);
        Tasks CreateTask(Tasks task);
        void UpdateTask(Tasks task);
        void DeleteTask(int id);
    }
}
