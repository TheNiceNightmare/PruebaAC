using ConsultorioSeguros.DataAccess;
using ConsultorioSeguros.Models;
using System.Collections.Generic;

namespace ConsultorioSeguros.BusinessLogic
{
    public class AseguradosService
    {
        private readonly AseguradoRepository _aseguradoRepository;
        private readonly AseguradoSeguroRepository _aseguradoSeguroRepository;

        // Constructor que acepta tanto AseguradoRepository como AseguradoSeguroRepository
        public AseguradosService(AseguradoRepository aseguradoRepository, AseguradoSeguroRepository aseguradoSeguroRepository)
        {
            _aseguradoRepository = aseguradoRepository;
            _aseguradoSeguroRepository = aseguradoSeguroRepository;
        }

        // Métodos para manejar Asegurado
        public IEnumerable<Asegurado> GetAllAsegurados()
        {
            return _aseguradoRepository.GetAll();
        }

        public Asegurado GetAseguradoById(int id)
        {
            return _aseguradoRepository.GetById(id);
        }

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

        public IEnumerable<Asegurado> GetAseguradosByCedula(string cedula)
        {
            return _aseguradoRepository.GetAseguradosByCedula(cedula);
        }

        public IEnumerable<Asegurado> GetAseguradosBySeguroCodigo(string codigo)
        {
            return _aseguradoRepository.GetAseguradosBySeguroCodigo(codigo);
        }

        // Métodos para manejar AseguradoSeguro
        public IEnumerable<AseguradoSeguro> GetAseguradosByCodigoSeguro(string codigo)
        {
            return _aseguradoSeguroRepository.GetAseguradosByCodigoSeguro(codigo);
        }
    }
}
