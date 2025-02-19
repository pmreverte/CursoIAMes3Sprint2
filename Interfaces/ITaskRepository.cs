using System.Collections.Generic;
using Sprint2.Models;

namespace Sprint2.Interfaces
{
    public interface ITaskRepository
    {
        (IEnumerable<TodoTask> Tasks, int TotalCount) GetAll(TaskFilter filter, int page, int pageSize);
        TodoTask GetById(int id);
        void Add(TodoTask task);
        void Update(TodoTask task);
        void Delete(int id);
        IEnumerable<string> GetCategories();
    }
}
