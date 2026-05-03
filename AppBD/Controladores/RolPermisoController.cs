using AppBD.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Controladores
{
    internal class RolPermisoController
    {
        RolPermisoRepositorio repo = new RolPermisoRepositorio();

        public async Task TogglePermiso(int rolId, int permisoId, bool activo)
        {
            if (activo)
                await repo.AsignarPermiso(rolId, permisoId);
            else
                await repo.QuitarPermiso(rolId, permisoId);
        }

        public async Task<List<int>> ObtenerPermisos(int rolId)
        {
            return await repo.ObtenerPermisosPorRol(rolId);
        }
    }
}
