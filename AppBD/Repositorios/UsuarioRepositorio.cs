using AppBD.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Repositorios
{
    internal class UsuarioRepositorio
    {
        private Conexion conexion;


        public UsuarioRepositorio()
        {
            conexion = new Conexion();
        }
        public async Task GuardarUsuario(Usuario usuario)
        {
            try
            {
                using (var control = conexion.GetConnectionString())
                {
                    await control.OpenAsync();
                    string query = "INSERT INTO `users`(`id`, `name`, `email`, `password`, `role_id`) VALUES (@id, @name, @email, @password, @role_id)";

                    using (var cmd = new MySqlCommand(query, control))
                    {
                        cmd.Parameters.AddWithValue("@id", usuario.Id);
                        cmd.Parameters.AddWithValue("@name", usuario.Name);
                        cmd.Parameters.AddWithValue("@email", usuario.Email);
                        cmd.Parameters.AddWithValue("@password", usuario.Password);
                        cmd.Parameters.AddWithValue("@role_id", usuario.RoleId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el rol: " + ex.Message);
            }
        }


        public async Task<List<Usuario>> Listar()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                using (var connection = conexion.GetConnectionString())
                {

                    await connection.OpenAsync();

                    string query = "SELECT * FROM users";
                    using (var command = new MySqlCommand(query, connection))
                    {

                        var data = await command.ExecuteReaderAsync();
                        while (await data.ReadAsync())
                        {
                            Usuario usuario = new Usuario()
                            {
                                Id = data.GetInt32(data.GetOrdinal("id")),
                                Name = data.GetString(data.GetOrdinal("name")),
                                Email = data.GetString(data.GetOrdinal("email")),
                                Password = data.GetString(data.GetOrdinal("password")),
                                RoleId = data.GetInt32(data.GetOrdinal("role_id"))
                            };
                            list.Add(usuario);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
