using System.Collections.Generic;

namespace ConsultorioSeguros.Models
{
    public class ResultadosConsultaViewModel
    {
        public IEnumerable<Seguro> Seguros { get; set; }
        public IEnumerable<Asegurado> Asegurados { get; set; }
    }
}
