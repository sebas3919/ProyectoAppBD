using AppBD.Modelos;
using AppBD.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Controladores
{
    internal class PermisosController
    {
        PermisosRepositorio permisosRepositorio = new PermisosRepositorio();

        public async Task GuardarPermiso(Permisos permiso)
        {



            if (permiso == null || permiso.Name.Trim() == "")
            {
                throw new ArgumentException("El permiso no puede ser nulo o vacío");
            }
            await permisosRepositorio.GuardarPermiso(permiso);

        }


        public async Task<List<Permisos>> Listar()
        {
            try
            {
                return await permisosRepositorio.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los permisos xd");
            }
        }


    }
}
