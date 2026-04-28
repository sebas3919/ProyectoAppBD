using AppBD.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Repositorios
{
    internal class PermisosRepositorio
    {

        private Conexion conexion;


        public PermisosRepositorio()
        {
            conexion = new Conexion();
        }
        public async Task GuardarPermiso(Permisos permiso)
        {
            try
            {
                using (var control = conexion.GetConnectionString())
                {
                    await control.OpenAsync();
                    string query = "INSERT INTO permisos (nombre) VALUES (@nombre)";

                    using (var cmd = new MySqlCommand(query, control))
                    {
                        cmd.Parameters.AddWithValue("@nombre", permiso.Name);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el permiso: " + ex.Message);
            }
        }


        public async Task<List<Permisos>> Listar()
        {
            List<Permisos> list = new List<Permisos>();
            try
            {
                using (var connection = conexion.GetConnectionString())
                {

                    await connection.OpenAsync();

                    string query = "SELECT * FROM permisos";
                    using (var command = new MySqlCommand(query, connection))
                    {

                        var data = await command.ExecuteReaderAsync();
                        while (await data.ReadAsync())
                        {
                            Permisos permiso = new Permisos()
                            {
                                Id = data.GetInt32(data.GetOrdinal("id")),
                                Name = data.GetString(data.GetOrdinal("nombre"))
                            };
                            list.Add(permiso);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;

            // Lógica para guardar el rol en la base de datos utilizando la conexión

        }

        /*internal async Task GuardarRol(Rol rol)
        {
            throw new NotImplementedException();
        }*/
    }
}
