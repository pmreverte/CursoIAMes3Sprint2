using System;
using System.Collections.Generic;
using System.Linq;
using Sprint2.Interfaces;
using Sprint2.Models;

namespace Sprint2.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static List<TodoTask> _tasks = new List<TodoTask>();
        private static int _nextId = 1;

        public (IEnumerable<TodoTask> Tasks, int TotalCount) GetAll(TaskFilter filter, int page, int pageSize)
        {
            var query = _tasks.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                query = query.Where(t => t.Description.Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                       (t.Category != null && t.Category.Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                       (t.Notes != null && t.Notes.Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Status == filter.Status.Value);
            }

            if (filter.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == filter.Priority.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                query = query.Where(t => t.Category == filter.Category);
            }

            if (filter.IsOverdue.HasValue)
            {
                var today = DateTime.UtcNow.Date;
                query = filter.IsOverdue.Value
                    ? query.Where(t => t.DueDate.HasValue && t.DueDate.Value.Date < today)
                    : query.Where(t => !t.DueDate.HasValue || t.DueDate.Value.Date >= today);
            }

            // Obtener el total antes de paginar
            int totalCount = query.Count();

            // Aplicar paginación
            var tasks = query
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (tasks, totalCount);
        }

        public TodoTask GetById(int id)
        {
            return _tasks.Find(t => t.Id == id);
        }

        public void Add(TodoTask task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
        }

        public void Update(TodoTask task)
        {
            var existingTask = _tasks.Find(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
                existingTask.Priority = task.Priority;
                existingTask.Category = task.Category;
                existingTask.Notes = task.Notes;
                existingTask.DueDate = task.DueDate;
            }
        }

        public void Delete(int id)
        {
            _tasks.RemoveAll(x => x.Id == id);
        }

        public IEnumerable<string> GetCategories()
        {
            return _tasks
                .Where(t => !string.IsNullOrEmpty(t.Category))
                .Select(t => t.Category)
                .Distinct()
                .OrderBy(c => c);
        }
    }
}
