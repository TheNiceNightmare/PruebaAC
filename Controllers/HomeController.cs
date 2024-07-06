using ConsultorioSeguros.BusinessLogic;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConsultorioSeguros.Controllers
{
    public class HomeController : Controller
    {
        private readonly AseguradosService _aseguradosService;
        private readonly SegurosService _segurosService;

        public HomeController(AseguradosService aseguradosService, SegurosService segurosService)
        {
            _aseguradosService = aseguradosService;
            _segurosService = segurosService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConsultarPorCedula(string cedula)
        {
            // Obtiene la lista de asegurados por cédula
            var asegurados = _aseguradosService.GetAseguradosByCedula(cedula);
            var resultado = new List<AseguradoSeguro>();

            foreach (var asegurado in asegurados)
            {
                // Obtiene los seguros asociados a cada asegurado
                var seguros = _segurosService.GetSegurosByAseguradoId(asegurado.Id);
                foreach (var seguro in seguros)
                {
                    // Llena el modelo de vista con la información obtenida
                    resultado.Add(new AseguradoSeguro
                    {
                        NombreUsuario = asegurado.Nombre,
                        NombreSeguro = seguro.Nombre,
                        CodigoSeguro = seguro.Codigo,
                        SumaAsegurada = seguro.SumaAsegurada,
                        Prima = seguro.Prima
                    });
                }
            }

            // Asegúrate de que el nombre de la vista es correcto y que el modelo es de tipo IEnumerable<AseguradoSeguro>
            return View("ResultadosConsulta", resultado);
        }

        [HttpPost]
        public IActionResult ConsultarPorCodigoSeguro(string codigoSeguro)
        {
            // Obtiene la lista de asegurados por código de seguro usando el nuevo método del servicio
            var aseguradosSeguros = _aseguradosService.GetAseguradosByCodigoSeguro(codigoSeguro);
            var resultado = new List<AseguradoSeguro>(aseguradosSeguros);

            // Asegúrate de que el nombre de la vista es correcto y que el modelo es de tipo IEnumerable<AseguradoSeguro>
            return View("ResultadosConsulta", resultado);
        }
    }
}
