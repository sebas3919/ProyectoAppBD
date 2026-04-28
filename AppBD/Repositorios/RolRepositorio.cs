using AppBD.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Repositorios
{
    internal class RolRepositorio
    {
        private Conexion conexion;


        public RolRepositorio()
        {
            conexion = new Conexion();
        }
        public async Task GuardarRol(Rol rol)
        {
            try
            {
                using (var control = conexion.GetConnectionString())
                {
                    await control.OpenAsync();
                    string query = "INSERT INTO Roles (name) VALUES (@name)";

                    using (var cmd = new MySqlCommand(query, control))
                    {
                        cmd.Parameters.AddWithValue("@name", rol.Name);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el rol: " + ex.Message);
            }
        }

       
        public async Task <List<Rol>>Listar()
        {
            List< Rol> list = new List< Rol>();
            try
            {
                using (var connection = conexion.GetConnectionString())
                {

                    await connection.OpenAsync();
                
                    string query = "SELECT * FROM roles";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        
                        var data = await command.ExecuteReaderAsync();
                        while (await data.ReadAsync())
                        {
                            Rol rol = new Rol()
                            {
                                Id = data.GetInt32(data.GetOrdinal("id")),
                                Name = data.GetString(data.GetOrdinal("name"))
                            };
                            list.Add(rol);
                        }

                    }
                   
                }

            }
            catch (Exception ex) {
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
