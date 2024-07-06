using ConsultorioSeguros.DataAccess;
using ConsultorioSeguros.Models;
using System.Collections.Generic;

namespace ConsultorioSeguros.BusinessLogic
{
    public class AseguradosService
    {
        private readonly AseguradoRepository _aseguradoRepository;
        private readonly AseguradoSeguroRepository _aseguradoSeguroRepository;

        // Constructor que inicializa las instancias de AseguradoRepository y AseguradoSeguroRepository
        public AseguradosService(AseguradoRepository aseguradoRepository, AseguradoSeguroRepository aseguradoSeguroRepository)
        {
            _aseguradoRepository = aseguradoRepository;
            _aseguradoSeguroRepository = aseguradoSeguroRepository;
        }

        // Obtiene todos los asegurados
        public IEnumerable<Asegurado> GetAllAsegurados()
        {
            return _aseguradoRepository.GetAll();
        }

        // Obtiene un asegurado por su ID
        public Asegurado GetAseguradoById(int id)
        {
            return _aseguradoRepository.GetById(id);
        }

        // Añade un nuevo asegurado
        public void AddAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Add(asegurado);
        }

        // Actualiza los datos de un asegurado existente
        public void UpdateAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Update(asegurado);
        }

        // Elimina un asegurado por su ID
        public void DeleteAsegurado(int id)
        {
            _aseguradoRepository.Delete(id);
        }

        // Obtiene asegurados por su cédula
        public IEnumerable<Asegurado> GetAseguradosByCedula(string cedula)
        {
            return _aseguradoRepository.GetAseguradosByCedula(cedula);
        }

        // Obtiene asegurados asociados a un seguro específico por el código del seguro
        public IEnumerable<Asegurado> GetAseguradosBySeguroCodigo(string codigo)
        {
            return _aseguradoRepository.GetAseguradosBySeguroCodigo(codigo);
        }

        // Obtiene la información de asegurados asociados a un seguro específico usando el código del seguro
        public IEnumerable<AseguradoSeguro> GetAseguradosByCodigoSeguro(string codigo)
        {
            return _aseguradoSeguroRepository.GetAseguradosByCodigoSeguro(codigo);
        }
    }
}
