using Microsoft.AspNetCore.Mvc;
using ConsultorioSeguros.Models;
using ConsultorioSeguros.BusinessLogic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ConsultorioSeguros.Controllers
{
    public class AseguradosController : Controller
    {
        private readonly AseguradosService _aseguradosService;

        // Constructor que inyecta el servicio AseguradosService
        public AseguradosController(AseguradosService aseguradosService)
        {
            _aseguradosService = aseguradosService;
        }

        // Acción para mostrar el formulario de creación de asegurado
        [HttpGet]
        public IActionResult Create()
        {
            // Pasa un modelo vacío a la vista para la creación de un nuevo asegurado
            return View(new Asegurado());
        }

        // Acción para manejar el envío del formulario de creación de asegurado
        [HttpPost]
        public IActionResult Create(Asegurado asegurado)
        {
            // Verifica si el modelo de asegurado es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio para añadir el asegurado a la base de datos
                _aseguradosService.AddAsegurado(asegurado);
                // Redirige a la vista deseada tras la creación exitosa
                return RedirectToAction("Index");
            }
            // Si el modelo no es válido, devuelve la vista con el modelo que contiene los errores de validación
            return View(asegurado);
        }

        // Acción para listar todos los asegurados
        public IActionResult Index()
        {
            // Obtiene todos los asegurados del servicio
            var asegurados = _aseguradosService.GetAllAsegurados();
            // Pasa la lista de asegurados a la vista para su visualización
            return View(asegurados);
        }

        // Acción para mostrar los detalles de un asegurado específico
        public IActionResult Details(int id)
        {
            // Obtiene el asegurado por ID del servicio
            var asegurado = _aseguradosService.GetAseguradoById(id);
            if (asegurado == null)
            {
                // Retorna un error 404 si no se encuentra el asegurado con el ID proporcionado
                return NotFound();
            }
            // Pasa el asegurado a la vista de detalles para su visualización
            return View(asegurado);
        }

        // Acción para mostrar el formulario de edición de un asegurado
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Obtiene el asegurado por ID del servicio
            var asegurado = _aseguradosService.GetAseguradoById(id);
            if (asegurado == null)
            {
                // Retorna un error 404 si no se encuentra el asegurado con el ID proporcionado
                return NotFound();
            }
            // Pasa el asegurado a la vista de edición para modificarlo
            return View(asegurado);
        }

        // Acción para manejar el envío del formulario de edición de un asegurado
        [HttpPost]
        public IActionResult Edit(Asegurado asegurado)
        {
            // Verifica si el modelo de asegurado es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio para actualizar los datos del asegurado en la base de datos
                _aseguradosService.UpdateAsegurado(asegurado);
                // Redirige a la vista deseada tras la edición exitosa
                return RedirectToAction("Index");
            }
            // Si el modelo no es válido, devuelve la vista con el modelo que contiene los errores de validación
            return View(asegurado);
        }

        // Acción para eliminar un asegurado
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Elimina el asegurado por ID del servicio
            _aseguradosService.DeleteAsegurado(id);
            // Redirige a la vista de lista de asegurados tras la eliminación
            return RedirectToAction("Index");
        }

        // Acción para mostrar el formulario de carga de archivos
        [HttpGet]
        public IActionResult LoadFromFile()
        {
            return View(); // Devuelve la vista de carga de archivos
        }

        // Acción para manejar el envío del archivo cargado
        [HttpPost]
        public IActionResult LoadFromFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;

                        using (var reader = new StreamReader(stream))
                        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                        {
                            // Lee los encabezados del archivo CSV
                            csv.Read();
                            csv.ReadHeader();

                            var headers = csv.HeaderRecord;

                            // Verifica los encabezados esperados
                            var requiredHeaders = new[] { "Id", "Cedula", "Nombre", "Telefono", "Edad" };
                            foreach (var header in requiredHeaders)
                            {
                                if (!headers.Contains(header))
                                {
                                    // Configura el mensaje de error para la vista de error
                                    ViewData["Title"] = "Error de Formato";
                                    ViewData["ErrorMessage"] = $"El archivo CSV debe contener el encabezado '{header}'.";
                                    return View("Error"); // Devuelve la vista de error
                                }
                            }

                            // Lee los registros del archivo CSV
                            var records = csv.GetRecords<Asegurado>().ToList();

                            // Procesa cada registro (puedes agregar lógica de validación aquí si es necesario)
                            foreach (var asegurado in records)
                            {
                                // Añade el asegurado a la base de datos o realiza otras acciones necesarias
                                _aseguradosService.AddAsegurado(asegurado);
                            }
                        }
                    }

                    // Redirige a la vista de lista de asegurados tras cargar el archivo
                    return RedirectToAction("Index");
                }
                catch (HeaderValidationException ex)
                {
                    // Captura excepciones específicas relacionadas con encabezados
                    ViewData["Title"] = "Error en el Formato del Archivo";
                    ViewData["ErrorMessage"] = "Error en el formato del archivo CSV: " + ex.Message;
                    return View("Error"); // Devuelve la vista de error
                }
                catch (Exception ex)    
                {
                    // Captura otras excepciones generales
                    ViewData["Title"] = "Error";
                    ViewData["ErrorMessage"] = "Ocurrió un error al procesar el archivo: " + ex.Message;
                    return View("Error"); // Devuelve la vista de error
                }
            }

            // Si no se seleccionó ningún archivo, añade un error al modelo y devuelve la vista para intentar de nuevo
            ModelState.AddModelError("", "Por favor selecciona un archivo.");
            return View(); // Devuelve la vista con el error
        }


    }
}
