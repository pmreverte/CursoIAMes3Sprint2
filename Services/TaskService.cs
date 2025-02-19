using System.Collections.Generic;
using Sprint2.Interfaces;
using Sprint2.Models;

namespace Sprint2.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TaskListViewModel GetTaskList(TaskFilter filter, int page, int pageSize)
        {
            var (tasks, totalCount) = _taskRepository.GetAll(filter, page, pageSize);

            return new TaskListViewModel
            {
                Tasks = tasks,
                Filter = filter,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalCount
                }
            };
        }

        public TodoTask GetTaskById(int id)
        {
            return _taskRepository.GetById(id);
        }

        public void CreateTask(TodoTask task)
        {
            _taskRepository.Add(task);
        }

        public void UpdateTask(TodoTask task)
        {
            _taskRepository.Update(task);
        }

        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }

        public IEnumerable<string> GetCategories()
        {
            return _taskRepository.GetCategories();
        }
    }
}
