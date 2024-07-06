using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace ConsultorioSeguros.DataAccess
{
    public class AseguradoSeguroRepository
    {
        private readonly string _connectionString;

        public AseguradoSeguroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<AseguradoSeguro> GetAseguradosByCodigoSeguro(string codigo)
        {
            var aseguradosSeguros = new List<AseguradoSeguro>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT a.Nombre AS NombreUsuario, s.Nombre AS NombreSeguro, s.Codigo AS CodigoSeguro, " +
                    "s.SumaAsegurada, s.Prima " +
                    "FROM Asegurados a " +
                    "INNER JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId " +
                    "INNER JOIN Seguros s ON s.Id = asg.SeguroId " +
                    "WHERE s.Codigo = @Codigo",
                    connection);

                command.Parameters.AddWithValue("@Codigo", codigo);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aseguradosSeguros.Add(new AseguradoSeguro
                        {
                            NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                            NombreSeguro = reader.GetString(reader.GetOrdinal("NombreSeguro")),
                            CodigoSeguro = reader.GetString(reader.GetOrdinal("CodigoSeguro")),
                            SumaAsegurada = reader.GetDecimal(reader.GetOrdinal("SumaAsegurada")),
                            Prima = reader.GetDecimal(reader.GetOrdinal("Prima"))
                        });
                    }
                }
            }

            return aseguradosSeguros;
        }
    }
}
