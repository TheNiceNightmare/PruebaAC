using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace ConsultorioSeguros.DataAccess
{
    public class AseguradoSeguroRepository
    {
        // Cadena de conexión a la base de datos
        private readonly string _connectionString;

        // Constructor que recibe la cadena de conexión para inicializar el repositorio
        public AseguradoSeguroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para obtener una lista de asegurados asociados a un seguro basado en el código del seguro
        public IEnumerable<AseguradoSeguro> GetAseguradosByCodigoSeguro(string codigo)
        {
            var aseguradosSeguros = new List<AseguradoSeguro>();

            // Abre una conexión a la base de datos
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Crea un comando SQL para obtener los asegurados y seguros asociados
                var command = new SqlCommand(
                    "SELECT a.Nombre AS NombreUsuario, s.Nombre AS NombreSeguro, s.Codigo AS CodigoSeguro, " +
                    "s.SumaAsegurada, s.Prima " +
                    "FROM Asegurados a " +
                    "INNER JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId " +
                    "INNER JOIN Seguros s ON s.Id = asg.SeguroId " +
                    "WHERE s.Codigo = @Codigo",
                    connection);

                // Agrega el parámetro del código del seguro al comando SQL
                command.Parameters.AddWithValue("@Codigo", codigo);

                // Ejecuta el comando y lee los datos obtenidos
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Crea un objeto AseguradoSeguro con los datos obtenidos y lo agrega a la lista
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

            // Retorna la lista de asegurados y seguros asociados
            return aseguradosSeguros;
        }
    }
}
