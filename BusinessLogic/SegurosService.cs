using ConsultorioSeguros.DataAccess;
using ConsultorioSeguros.Models;
using System.Collections.Generic;

namespace ConsultorioSeguros.BusinessLogic
{
    public class SegurosService
    {
        private readonly SeguroRepository _seguroRepository;
        private readonly AseguradoRepository _aseguradoRepository;

        // Constructor que inicializa las instancias de SeguroRepository y AseguradoRepository
        public SegurosService(string connectionString)
        {
            _seguroRepository = new SeguroRepository(connectionString);
            _aseguradoRepository = new AseguradoRepository(connectionString);
        }

        // Métodos para manejar seguros

        // Añade un nuevo seguro
        public void AddSeguro(Seguro seguro)
        {
            _seguroRepository.Add(seguro);
        }

        // Actualiza un seguro existente
        public void UpdateSeguro(Seguro seguro)
        {
            _seguroRepository.Update(seguro);
        }

        // Elimina un seguro por su ID
        public void DeleteSeguro(int id)
        {
            _seguroRepository.Delete(id);
        }

        // Obtiene un seguro por su ID
        public Seguro GetSeguroById(int id)
        {
            return _seguroRepository.GetById(id);
        }

        // Obtiene todos los seguros
        public IEnumerable<Seguro> GetAllSeguros()
        {
            return _seguroRepository.GetAll();
        }

        // Obtiene los seguros asociados a un asegurado específico por su ID
        public IEnumerable<Seguro> GetSegurosByAseguradoId(int aseguradoId)
        {
            return _seguroRepository.GetSegurosByAseguradoId(aseguradoId);
        }

        // Métodos para manejar asegurados

        // Añade un nuevo asegurado
        public void AddAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Add(asegurado);
        }

        // Actualiza un asegurado existente
        public void UpdateAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Update(asegurado);
        }

        // Elimina un asegurado por su ID
        public void DeleteAsegurado(int id)
        {
            _aseguradoRepository.Delete(id);
        }

        // Obtiene un asegurado por su ID
        public Asegurado GetAseguradoById(int id)
        {
            return _aseguradoRepository.GetById(id);
        }

        // Obtiene todos los asegurados
        public IEnumerable<Asegurado> GetAllAsegurados()
        {
            return _aseguradoRepository.GetAll();
        }

        // Obtiene asegurados por su cédula
        public IEnumerable<Asegurado> GetAseguradosByCedula(string cedula)
        {
            return _aseguradoRepository.GetAseguradosByCedula(cedula);
        }

        // Obtiene asegurados asociados a un seguro específico por el ID del seguro
        public IEnumerable<Asegurado> GetAseguradosBySeguroId(int seguroId)
        {
            return _aseguradoRepository.GetAseguradosBySeguroId(seguroId);
        }

        // Obtiene asegurados por el código del seguro
        public IEnumerable<Asegurado> GetAseguradosPorCodigo(string codigo)
        {
            return _aseguradoRepository.GetAseguradosByCedula(codigo);
        }
    }
}
