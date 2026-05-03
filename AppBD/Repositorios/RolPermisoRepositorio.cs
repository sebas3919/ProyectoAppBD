using AppBD.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Repositorios
{
    internal class RolPermisoRepositorio
    {
        private Conexion conexion;

        public RolPermisoRepositorio()
        {
            conexion = new Conexion();
        }
        public async Task AsignarPermiso(int rolId, int permisoId)
        {

            try
            {
                using (var conn = conexion.GetConnectionString())
                {
                    await conn.OpenAsync();

                    string query = "INSERT IGNORE INTO role_permisos (role_id, permiso_id) VALUES (@rol, @permiso)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@rol", rolId);
                        cmd.Parameters.AddWithValue("@permiso", permisoId);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

            }
            
            catch (Exception ex)
            {
                throw new Exception("Error al asignar el permiso: " + ex.Message);
            }
        }

        public async Task QuitarPermiso(int rolId, int permisoId)
        {
            using (var conn = conexion.GetConnectionString())
            {
                await conn.OpenAsync();

                string query = "DELETE FROM role_permisos WHERE role_id = @rol AND permiso_id = @permiso";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@rol", rolId);
                    cmd.Parameters.AddWithValue("@permiso", permisoId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<int>> ObtenerPermisosPorRol(int rolId)
        {
            List<int> permisos = new List<int>();

            using (var conn = conexion.GetConnectionString())
            {
                await conn.OpenAsync();

                string query = "SELECT permiso_id FROM role_permisos WHERE role_id = @rol";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@rol", rolId);

                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        permisos.Add(reader.GetInt32(0));
                    }
                }
            }

            return permisos;
        }
    }
}
