using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioSeguros.Models
{
    public class Seguro
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal SumaAsegurada { get; set; }
        public decimal Prima { get; set; }
        public ICollection<Asegurado> Asegurados { get; set; } = new List<Asegurado>();
    }
}
