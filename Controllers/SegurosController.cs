using ConsultorioSeguros.BusinessLogic;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class SegurosController : Controller
    {
        private readonly SegurosService _segurosService;

        public SegurosController(SegurosService segurosService)
        {
            _segurosService = segurosService;
        }

        public IActionResult Index()
        {
            var seguros = _segurosService.GetAllSeguros();
            return View(seguros);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seguro seguro)
        {
            if (ModelState.IsValid)
            {
                _segurosService.AddSeguro(seguro);
                return RedirectToAction(nameof(Index));
            }
            return View(seguro);
        }

        public IActionResult Edit(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound();
            }
            return View(seguro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seguro seguro)
        {
            if (id != seguro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _segurosService.UpdateSeguro(seguro);
                return RedirectToAction(nameof(Index));
            }
            return View(seguro);
        }

        public IActionResult Delete(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound();
            }
            return View(seguro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _segurosService.DeleteSeguro(id);
            return RedirectToAction(nameof(Index));
        }

        // Acción para mostrar detalles de un seguro
        public IActionResult Details(int id)
        {
            var seguro = _segurosService.GetSeguroById(id);
            if (seguro == null)
            {
                return NotFound();
            }
            return View(seguro);
        }
    }
}
