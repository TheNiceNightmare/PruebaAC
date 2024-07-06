using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ConsultorioSeguros.DataAccess
{
    public class SeguroRepository
    {
        private readonly string _connectionString;

        public SeguroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Seguro seguro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Seguros (Nombre, Codigo, SumaAsegurada, Prima) VALUES (@Nombre, @Codigo, @SumaAsegurada, @Prima)", connection);
                command.Parameters.AddWithValue("@Nombre", seguro.Nombre);
                command.Parameters.AddWithValue("@Codigo", seguro.Codigo);
                command.Parameters.AddWithValue("@SumaAsegurada", seguro.SumaAsegurada);
                command.Parameters.AddWithValue("@Prima", seguro.Prima);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Seguro> GetAll()
        {
            var seguros = new List<Seguro>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Seguros", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seguros.Add(new Seguro
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            SumaAsegurada = reader.GetDecimal(reader.GetOrdinal("SumaAsegurada")),
                            Prima = reader.GetDecimal(reader.GetOrdinal("Prima"))
                        });
                    }
                }
            }

            return seguros;
        }

        public Seguro GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Seguros WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Seguro
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            SumaAsegurada = reader.GetDecimal(reader.GetOrdinal("SumaAsegurada")),
                            Prima = reader.GetDecimal(reader.GetOrdinal("Prima"))
                        };
                    }
                }
            }

            return null;
        }

        public void Update(Seguro seguro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Seguros SET Nombre = @Nombre, Codigo = @Codigo, SumaAsegurada = @SumaAsegurada, Prima = @Prima WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", seguro.Id);
                command.Parameters.AddWithValue("@Nombre", seguro.Nombre);
                command.Parameters.AddWithValue("@Codigo", seguro.Codigo);
                command.Parameters.AddWithValue("@SumaAsegurada", seguro.SumaAsegurada);
                command.Parameters.AddWithValue("@Prima", seguro.Prima);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Seguros WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Seguro> GetSegurosByAseguradoId(int aseguradoId)
        {
            var seguros = new List<Seguro>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT s.* FROM Seguros s JOIN AseguradosSeguros asg ON s.Id = asg.SeguroId WHERE asg.AseguradoId = @AseguradoId", connection);
                command.Parameters.AddWithValue("@AseguradoId", aseguradoId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seguros.Add(new Seguro
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            SumaAsegurada = reader.GetDecimal(reader.GetOrdinal("SumaAsegurada")),
                            Prima = reader.GetDecimal(reader.GetOrdinal("Prima"))
                        });
                    }
                }
            }

            return seguros;
        }


        public IEnumerable<Asegurado> GetAseguradosByCodigo(string codigo)
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT a.*
                    FROM Asegurados a
                    JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId
                    JOIN Seguros s ON s.Id = asg.SeguroId
                    WHERE s.Codigo = @Codigo", connection);
                command.Parameters.AddWithValue("@Codigo", codigo);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        asegurados.Add(new Asegurado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Cedula = reader.GetString(reader.GetOrdinal("Cedula")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                            Edad = reader.GetInt32(reader.GetOrdinal("Edad"))
                        });
                    }
                }
            }

            return asegurados;
        }
    }
}
