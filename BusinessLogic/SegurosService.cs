using ConsultorioSeguros.DataAccess;
using ConsultorioSeguros.Models;
using System.Collections.Generic;

namespace ConsultorioSeguros.BusinessLogic
{
    public class SegurosService
    {
        private readonly SeguroRepository _seguroRepository;
        private readonly AseguradoRepository _aseguradoRepository;

        public SegurosService(string connectionString)
        {
            _seguroRepository = new SeguroRepository(connectionString);
            _aseguradoRepository = new AseguradoRepository(connectionString);
        }


        // Métodos para manejar seguros

        public void AddSeguro(Seguro seguro)
        {
            _seguroRepository.Add(seguro);
        }

        public void UpdateSeguro(Seguro seguro)
        {
            _seguroRepository.Update(seguro);
        }

        public void DeleteSeguro(int id)
        {
            _seguroRepository.Delete(id);
        }

        public Seguro GetSeguroById(int id)
        {
            return _seguroRepository.GetById(id);
        }

        public IEnumerable<Seguro> GetAllSeguros()
        {
            return _seguroRepository.GetAll();
        }

        public IEnumerable<Seguro> GetSegurosByAseguradoId(int aseguradoId)
        {
            return _seguroRepository.GetSegurosByAseguradoId(aseguradoId);
        }

        // Métodos para manejar asegurados

        public void AddAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Add(asegurado);
        }

        public void UpdateAsegurado(Asegurado asegurado)
        {
            _aseguradoRepository.Update(asegurado);
        }

        public void DeleteAsegurado(int id)
        {
            _aseguradoRepository.Delete(id);
        }

        public Asegurado GetAseguradoById(int id)
        {
            return _aseguradoRepository.GetById(id);
        }

        public IEnumerable<Asegurado> GetAllAsegurados()
        {
            return _aseguradoRepository.GetAll();
        }

        public IEnumerable<Asegurado> GetAseguradosByCedula(string cedula)
        {
            return _aseguradoRepository.GetAseguradosByCedula(cedula);
        }

        public IEnumerable<Asegurado> GetAseguradosBySeguroId(int seguroId)
        {
            return _aseguradoRepository.GetAseguradosBySeguroId(seguroId);
        }

        public IEnumerable<Asegurado> GetAseguradosPorCodigo(string codigo)
        {
            return _aseguradoRepository.GetAseguradosByCedula(codigo); // Ajusta si el código es diferente
        }
    }
}
