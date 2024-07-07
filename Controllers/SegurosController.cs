using ConsultorioSeguros.BusinessLogic;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class SegurosController : Controller
    {
        // Servicio inyectado para manejar la lógica de negocio relacionada con seguros
        private readonly SegurosService _segurosService;

        // Constructor que recibe el servicio de seguros como parámetro para inyectarlo
        public SegurosController(SegurosService segurosService)
        {
            _segurosService = segurosService;
        }

        // Acción para mostrar la lista de todos los seguros
        public IActionResult Index()
        {
            var seguros = _segurosService.GetAllSeguros();
            return View(seguros); // Pasa la lista de seguros a la vista
        }

        // Acción para mostrar el formulario de creación de un nuevo seguro
        public IActionResult Create()
        {
            return View();
        }

        // Acción para procesar la creación de un nuevo seguro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seguro seguro)
        {
            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                _segurosService.AddSeguro(seguro); // Agrega el nuevo seguro
                return RedirectToAction(nameof(Index)); // Redirige a la lista de seguros
            }
            return View(seguro); // Retorna la vista con el modelo para corregir errores
        }

        // Acción para mostrar el formulario de edición de un seguro existente
        public IActionResult Edit(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound(); // Retorna un error 404 si el seguro no se encuentra
            }
            return View(seguro); // Pasa el seguro a la vista de edición
        }

        // Acción para procesar la actualización de un seguro existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seguro seguro)
        {
            if (id != seguro.Id)
            {
                return NotFound(); // Retorna un error 404 si el ID no coincide
            }

            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                _segurosService.UpdateSeguro(seguro); // Actualiza el seguro
                return RedirectToAction(nameof(Index)); // Redirige a la lista de seguros
            }
            return View(seguro); // Retorna la vista con el modelo para corregir errores
        }

        // Acción para mostrar la confirmación de eliminación de un seguro
        public IActionResult Delete(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound(); // Retorna un error 404 si el seguro no se encuentra
            }
            return View(seguro); // Pasa el seguro a la vista de confirmación de eliminación
        }

        // Acción para procesar la eliminación de un seguro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _segurosService.DeleteSeguro(id); // Elimina el seguro
            return RedirectToAction(nameof(Index)); // Redirige a la lista de seguros
        }

        // Acción para mostrar detalles de un seguro específico
        public IActionResult Details(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound(); // Retorna un error 404 si el seguro no se encuentra
            }
            return View(seguro); // Pasa el seguro a la vista de detalles
        }
    }
}
