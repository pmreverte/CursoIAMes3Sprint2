using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sprint2.Interfaces;
using Sprint2.Models;

namespace Sprint2.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;
        private const int DefaultPageSize = 10;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public IActionResult Index(TaskFilter filter, int page = 1)
        {
            try
            {
                var viewModel = _taskService.GetTaskList(filter, page, DefaultPageSize);
                ViewBag.Categories = _taskService.GetCategories();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de tareas");
                TempData["Error"] = "Ha ocurrido un error al cargar las tareas.";
                return View(new TaskListViewModel());
            }
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _taskService.GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoTask task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.CreateTask(task);
                    TempData["Success"] = "Tarea creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Categories = _taskService.GetCategories();
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la tarea");
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al crear la tarea.");
                ViewBag.Categories = _taskService.GetCategories();
                return View(task);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                ViewBag.Categories = _taskService.GetCategories();
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la tarea para editar");
                TempData["Error"] = "Ha ocurrido un error al cargar la tarea.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TodoTask task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.UpdateTask(task);
                    TempData["Success"] = "Tarea actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Categories = _taskService.GetCategories();
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la tarea");
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al actualizar la tarea.");
                ViewBag.Categories = _taskService.GetCategories();
                return View(task);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la tarea para eliminar");
                TempData["Error"] = "Ha ocurrido un error al cargar la tarea.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
                TempData["Success"] = "Tarea eliminada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la tarea");
                TempData["Error"] = "Ha ocurrido un error al eliminar la tarea.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, TodoTaskStatus newStatus)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }

                task.Status = newStatus;
                _taskService.UpdateTask(task);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado de la tarea");
                return Json(new { success = false, message = "Error al actualizar el estado." });
            }
        }
    }
}
