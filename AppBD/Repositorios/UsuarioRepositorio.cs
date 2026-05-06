using AppBD.DAO;
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

                    string query = @"INSERT INTO users(name, email, password, role_id)
                             VALUES (@name, @email, @password, @role_id)";

                    using (var cmd = new MySqlCommand(query, control))
                    {
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
                throw new Exception("Error al guardar usuario: " + ex.Message);
            }
        }


        public async Task<List<UserListDAO>> Listar()
        {
            List<UserListDAO> list = new List<UserListDAO>();
            try
            {
                using (var connection = conexion.GetConnectionString())
                {

                    await connection.OpenAsync();

                    string query = "SELECT users.id, users.name as user_name, users.email, roles.name as role_name FROM `users` JOIN `roles` on `roles`.id = `users`.role_id";
                    using (var command = new MySqlCommand(query, connection))
                    {

                        var data = await command.ExecuteReaderAsync();
                        while (await data.ReadAsync())
                        {
                            UserListDAO usuario = new UserListDAO()
                            {
                                Id = data.GetInt32(data.GetOrdinal("id")),
                                UserName = data.GetString(data.GetOrdinal("user_name")),
                                Email = data.GetString(data.GetOrdinal("email")),
                                Rolename = data.GetString(data.GetOrdinal("role_name"))
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

        public async Task<Usuario> ObtenerPorEmail(string email)
        {
            try
            {
                using (var connection = conexion.GetConnectionString())
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM users WHERE email = @email LIMIT 1";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Usuario
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Email = reader.GetString(reader.GetOrdinal("email")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    RoleId = reader.GetInt32(reader.GetOrdinal("role_id"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = conexion.GetConnectionString())
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM users WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }

        public async Task<bool> EditarUsuario(Usuario usuario)
        {
            try
            {
                using (var connection = conexion.GetConnectionString())
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE users SET name = @name, email = @email,  password = @password, role_id = @role_id WHERE id = @id";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", usuario.Id);
                        cmd.Parameters.AddWithValue("@name", usuario.Name);
                        cmd.Parameters.AddWithValue("@email", usuario.Email);
                        cmd.Parameters.AddWithValue("@password", usuario.Password);
                        cmd.Parameters.AddWithValue("@role_id", usuario.RoleId);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar usuario: " + ex.Message);
            }
        }





    }
}
