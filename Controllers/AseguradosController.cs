using Microsoft.AspNetCore.Mvc;
using ConsultorioSeguros.Models;
using ConsultorioSeguros.BusinessLogic; // Asegúrate de que la referencia sea correcta

namespace ConsultorioSeguros.Controllers
{
    public class AseguradosController : Controller
    {
        private readonly AseguradosService _aseguradosService;

        // Constructor que inyecta el servicio
        public AseguradosController(AseguradosService aseguradosService)
        {
            _aseguradosService = aseguradosService;
        }

        // Acción para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Create()
        {
            // Pasa un modelo vacío a la vista para la creación
            return View(new Asegurado());
        }

        // Acción para manejar el envío del formulario de creación
        [HttpPost]
        public IActionResult Create(Asegurado asegurado)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio para añadir el asegurado
                _aseguradosService.AddAsegurado(asegurado);
                // Redirige a la vista deseada tras la creación exitosa
                return RedirectToAction("Index"); // Cambia "Index" al nombre de la acción a la que deseas redirigir
            }
            // Devuelve la vista con el modelo que contiene los errores
            return View(asegurado);
        }

        // Acción para listar todos los asegurados
        public IActionResult Index()
        {
            // Obtiene todos los asegurados del servicio
            var asegurados = _aseguradosService.GetAllAsegurados();
            // Pasa la lista de asegurados a la vista
            return View(asegurados);
        }

        // Acción para mostrar los detalles de un asegurado
        public IActionResult Details(int id)
        {
            // Obtiene el asegurado por ID del servicio
            var asegurado = _aseguradosService.GetAseguradoById(id);
            if (asegurado == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el asegurado
            }
            // Pasa el asegurado a la vista de detalles
            return View(asegurado);
        }

        // Acción para mostrar el formulario de edición
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Obtiene el asegurado por ID del servicio
            var asegurado = _aseguradosService.GetAseguradoById(id);
            if (asegurado == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el asegurado
            }
            // Pasa el asegurado a la vista de edición
            return View(asegurado);
        }

        // Acción para manejar el envío del formulario de edición
        [HttpPost]
        public IActionResult Edit(Asegurado asegurado)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio para actualizar el asegurado
                _aseguradosService.UpdateAsegurado(asegurado);
                // Redirige a la vista deseada tras la edición exitosa
                return RedirectToAction("Index"); // Cambia "Index" al nombre de la acción a la que deseas redirigir
            }
            // Devuelve la vista con el modelo que contiene los errores
            return View(asegurado);
        }

        // Acción para eliminar un asegurado
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Elimina el asegurado por ID del servicio
            _aseguradosService.DeleteAsegurado(id);
            // Redirige a la vista de lista de asegurados
            return RedirectToAction("Index");
        }

        // Acción para cargar asegurados desde un archivo
        [HttpGet]
        public IActionResult LoadFromFile()
        {
            return View(); // Devuelve una vista donde el usuario puede subir el archivo
        }

        [HttpPost]
        public IActionResult LoadFromFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    // Aquí puedes agregar lógica para procesar el archivo y cargar asegurados
                    // stream.ToArray() te dará el contenido del archivo
                }

                // Redirige a la vista de lista de asegurados tras cargar el archivo
                return RedirectToAction("Index");
            }

            // Si no se seleccionó ningún archivo, devuelve la vista con un mensaje de error
            ModelState.AddModelError("", "Por favor selecciona un archivo.");
            return View();
        }
    }
}
