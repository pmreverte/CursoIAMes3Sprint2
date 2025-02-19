# 📝 Aplicación de Gestión de Tareas

Este proyecto es una aplicación web de gestión de tareas desarrollada en **ASP.NET Core MVC**. Permite la creación, edición, eliminación y visualización de tareas con funcionalidades avanzadas como **filtrado, paginación y validaciones de datos**.

## 🚀 Características Principales

- ✅ **Listado de tareas** con filtrado por estado, prioridad, categoría y búsqueda de términos.
- ✏️ **Creación y edición de tareas** con validaciones.
- 🗑️ **Eliminación y actualización de estado** de tareas.
- 📄 **Paginación** para manejar grandes volúmenes de datos.

## 🏗️ Arquitectura del Proyecto

El proyecto está estructurado siguiendo el patrón **MVC (Model-View-Controller)**:

### **1. 🎮 Controlador**

- ``: Maneja las solicitudes HTTP relacionadas con la gestión de tareas.

### **2. 🛠️ Servicios y Repositorios**

- ``: Define las operaciones de acceso a datos.
- ``: Implementa la persistencia en memoria.
- ``: Define la lógica de negocio.
- ``: Implementa la lógica de negocio sobre las tareas.

### **3. 🗂️ Modelos**

- ``: Modelo que representa una tarea, con validaciones y atributos personalizados.
- ``: Contiene la lista de tareas y los datos de paginación.

### **4. 🎨 Vistas**

- 📌 ``: Listado de tareas.
- 📝 ``: Formulario para agregar una nueva tarea.
- ✏️ ``: Edición de una tarea existente.
- ❌ ``: Confirmación de eliminación de tareas.
- 📎 **Vistas Parciales**: `_Pagination.cshtml`, `_Filters.cshtml`, `_TaskCard.cshtml`, `_Notifications.cshtml`.

### **5. ⚙️ Configuración**

- ``: Configura los servicios y rutas del proyecto.
- `` y ``: Contienen las configuraciones de la aplicación, como niveles de logging y entornos de desarrollo.
- ``: Define las URLs de ejecución en entornos locales.

## 🔧 Requisitos Previos

- 📌 [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) o superior.
- 🖥️ Visual Studio 2022 o Visual Studio Code.

## 📦 Instalación y Ejecución

1. **📥 Clonar el repositorio:**

   ```bash
   git clone https://github.com/tu-usuario/tu-repositorio.git
   ```

2. **📦 Restaurar dependencias:**

   ```bash
   dotnet restore
   ```

3. **▶️ Ejecutar la aplicación:**

   ```bash
   dotnet run
   ```

   La aplicación estará disponible en `http://localhost:5080` (según `launchSettings.json`).

##

