using System.Collections.Generic;

namespace Sprint2.Models
{
    public class TaskListViewModel
    {
        public IEnumerable<TodoTask> Tasks { get; set; } = new List<TodoTask>();
        public TaskFilter Filter { get; set; } = new();
        public PaginationInfo Pagination { get; set; } = new();
    }

    public class TaskFilter
    {
        public string? SearchTerm { get; set; }
        public TodoTaskStatus? Status { get; set; }
        public Priority? Priority { get; set; }
        public string? Category { get; set; }
        public bool? IsOverdue { get; set; }
    }

    public class PaginationInfo
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (TotalItems + PageSize - 1) / PageSize;

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
