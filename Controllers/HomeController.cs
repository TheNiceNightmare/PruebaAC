using ConsultorioSeguros.BusinessLogic;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConsultorioSeguros.Controllers
{
    public class HomeController : Controller
    {
        // Servicios inyectados para manejar la lógica de negocio relacionada con asegurados y seguros
        private readonly AseguradosService _aseguradosService;
        private readonly SegurosService _segurosService;

        // Constructor que recibe los servicios como parámetros para inyectarlos
        public HomeController(AseguradosService aseguradosService, SegurosService segurosService)
        {
            _aseguradosService = aseguradosService;
            _segurosService = segurosService;
        }

        // Acción para la vista principal del controlador
        public IActionResult Index()
        {
            return View();
        }

        // Acción para consultar asegurados por cédula
        [HttpPost]
        public IActionResult ConsultarPorCedula(string cedula)
        {
            // Obtiene la lista de asegurados basados en la cédula proporcionada
            var asegurados = _aseguradosService.GetAseguradosByCedula(cedula);
            var resultado = new List<AseguradoSeguro>();

            // Itera sobre los asegurados y obtiene los seguros asociados a cada uno
            foreach (var asegurado in asegurados)
            {
                var seguros = _segurosService.GetSegurosByAseguradoId(asegurado.Id);
                foreach (var seguro in seguros)
                {
                    // Llena el modelo de vista con la información del asegurado y el seguro
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

            // Retorna la vista "ResultadosConsulta" con la lista de asegurado y seguro como modelo
            return View("ResultadosConsulta", resultado);
        }

        // Acción para consultar asegurados por código de seguro
        [HttpPost]
        public IActionResult ConsultarPorCodigoSeguro(string codigoSeguro)
        {
            // Obtiene la lista de asegurados asociados al código de seguro proporcionado
            var aseguradosSeguros = _aseguradosService.GetAseguradosByCodigoSeguro(codigoSeguro);
            var resultado = new List<AseguradoSeguro>(aseguradosSeguros);

            // Retorna la vista "ResultadosConsulta" con la lista de asegurado y seguro como modelo
            return View("ResultadosConsulta", resultado);
        }
    }
}
