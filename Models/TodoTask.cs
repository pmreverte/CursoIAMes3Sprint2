using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint2.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 200 caracteres")]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public TodoTaskStatus Status { get; set; } = TodoTaskStatus.Pending;

        [Display(Name = "Fecha de Creación")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "La fecha de vencimiento debe ser futura")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "La prioridad es obligatoria")]
        [Display(Name = "Prioridad")]
        public Priority Priority { get; set; } = Priority.Medium;

        [StringLength(50)]
        [Display(Name = "Categoría")]
        public string? Category { get; set; }

        [StringLength(500)]
        [Display(Name = "Notas")]
        public string? Notes { get; set; }

        [Display(Name = "Completada")]
        public bool IsCompleted => Status == TodoTaskStatus.Completed;

        [Display(Name = "Vencida")]
        public bool IsOverdue => DueDate.HasValue && DueDate.Value.Date < DateTime.UtcNow.Date;

        public TodoTask() { }

        public TodoTask(string description)
        {
            Description = description;
        }
    }

    public enum TodoTaskStatus
    {
        [Display(Name = "Pendiente")]
        Pending,
        [Display(Name = "En Progreso")]
        InProgress,
        [Display(Name = "Completada")]
        Completed
    }

    public enum Priority
    {
        [Display(Name = "Baja")]
        Low,
        [Display(Name = "Media")]
        Medium,
        [Display(Name = "Alta")]
        High,
        [Display(Name = "Urgente")]
        Urgent
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date.Date >= DateTime.UtcNow.Date;
            }
            return true; // Null is valid
        }
    }
}
