using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace ConsultorioSeguros.DataAccess
{
    public class AseguradoRepository
    {
        private readonly string _connectionString;

        public AseguradoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Asegurado asegurado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Asegurados (Cedula, Nombre, Telefono, Edad) VALUES (@Cedula, @Nombre, @Telefono, @Edad)", connection);
                command.Parameters.AddWithValue("@Cedula", asegurado.Cedula);
                command.Parameters.AddWithValue("@Nombre", asegurado.Nombre);
                command.Parameters.AddWithValue("@Telefono", asegurado.Telefono);
                command.Parameters.AddWithValue("@Edad", asegurado.Edad);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Asegurado> GetAll()
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Asegurados", connection);
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

        public Asegurado GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Asegurados WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Asegurado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Cedula = reader.GetString(reader.GetOrdinal("Cedula")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                            Edad = reader.GetInt32(reader.GetOrdinal("Edad"))
                        };
                    }
                }
            }

            return null;
        }

        public void Update(Asegurado asegurado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Asegurados SET Cedula = @Cedula, Nombre = @Nombre, Telefono = @Telefono, Edad = @Edad WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", asegurado.Id);
                command.Parameters.AddWithValue("@Cedula", asegurado.Cedula);
                command.Parameters.AddWithValue("@Nombre", asegurado.Nombre);
                command.Parameters.AddWithValue("@Telefono", asegurado.Telefono);
                command.Parameters.AddWithValue("@Edad", asegurado.Edad);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Asegurados WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Asegurado> GetAseguradosByCedula(string cedula)
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Asegurados WHERE Cedula = @Cedula", connection);
                command.Parameters.AddWithValue("@Cedula", cedula);

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

        public IEnumerable<Asegurado> GetAseguradosBySeguroId(int seguroId)
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT a.* 
                    FROM Asegurados a
                    JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId
                    WHERE asg.SeguroId = @SeguroId", connection);
                command.Parameters.AddWithValue("@SeguroId", seguroId);

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
        public IEnumerable<Asegurado> GetAseguradosBySeguroCodigo(string codigo)
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                // Definir la consulta SQL con el parámetro
                var command = new SqlCommand(
                    "SELECT a.* FROM Asegurados a " +
                    "JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId " +
                    "JOIN Seguros s ON asg.SeguroId = s.Id " +
                    "WHERE s.Codigo = @Codigo",
                    connection
                );

                // Añadir el parámetro con tipo específico
                command.Parameters.Add("@Codigo", SqlDbType.NVarChar, 50).Value = codigo;

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var asegurado = new Asegurado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Cedula = reader.GetString(reader.GetOrdinal("Cedula"))
                            // Añadir otras propiedades necesarias
                        };

                        asegurados.Add(asegurado);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
                }
            }

            return asegurados;
        }
    }
}
