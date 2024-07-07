using ConsultorioSeguros.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace ConsultorioSeguros.DataAccess
{
    public class AseguradoRepository
    {
        // Cadena de conexión a la base de datos
        private readonly string _connectionString;

        // Constructor que recibe la cadena de conexión para inicializar el repositorio
        public AseguradoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para agregar un nuevo asegurado a la base de datos
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
                command.ExecuteNonQuery(); // Ejecuta la inserción en la base de datos
            }
        }

        // Método para obtener todos los asegurados de la base de datos
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
                        // Lee los datos de cada asegurado y los añade a la lista
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

        // Método para obtener un asegurado por su ID
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
                        // Lee los datos del asegurado y lo retorna
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

            return null; // Retorna null si el asegurado no se encuentra
        }

        // Método para actualizar los datos de un asegurado en la base de datos
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
                command.ExecuteNonQuery(); // Ejecuta la actualización en la base de datos
            }
        }

        // Método para eliminar un asegurado de la base de datos por su ID
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Asegurados WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery(); // Ejecuta la eliminación en la base de datos
            }
        }

        // Método para obtener asegurados por su cédula
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
                        // Lee los datos de cada asegurado con la cédula especificada y los añade a la lista
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

        // Método para obtener asegurados asociados a un seguro por el ID del seguro
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
                        // Lee los datos de cada asegurado asociado al seguro especificado y los añade a la lista
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

        // Método para obtener asegurados asociados a un seguro por el código del seguro
        public IEnumerable<Asegurado> GetAseguradosBySeguroCodigo(string codigo)
        {
            var asegurados = new List<Asegurado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT a.* FROM Asegurados a " +
                    "JOIN AseguradosSeguros asg ON a.Id = asg.AseguradoId " +
                    "JOIN Seguros s ON asg.SeguroId = s.Id " +
                    "WHERE s.Codigo = @Codigo",
                    connection
                );
                command.Parameters.Add("@Codigo", SqlDbType.NVarChar, 50).Value = codigo;

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Lee los datos de cada asegurado asociado al seguro con el código especificado y los añade a la lista
                        var asegurado = new Asegurado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Cedula = reader.GetString(reader.GetOrdinal("Cedula"))
                        };

                        asegurados.Add(asegurado);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones en caso de error en la consulta
                    Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
                }
            }

            return asegurados;
        }
    }
}
