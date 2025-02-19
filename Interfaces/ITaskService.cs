using System.Collections.Generic;
using Sprint2.Models;

namespace Sprint2.Interfaces
{
    public interface ITaskService
    {
        TaskListViewModel GetTaskList(TaskFilter filter, int page, int pageSize);
        TodoTask GetTaskById(int id);
        void CreateTask(TodoTask task);
        void UpdateTask(TodoTask task);
        void DeleteTask(int id);
        IEnumerable<string> GetCategories();
    }
}
