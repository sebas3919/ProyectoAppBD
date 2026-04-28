using AppBD.Modelos;
using AppBD.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Controladores
{
    internal class RolesController
    {
        RolRepositorio rolRepositorio = new RolRepositorio();

        public async Task GuardarRol(Rol rol)
        {



            if (rol == null || rol.Name.Trim() == "")
            {
                throw new ArgumentException("El rol no puede ser nulo o vacío");
            }
            await rolRepositorio.GuardarRol(rol);


        }


        public async Task<List<Rol>> Listar()
        {
            try
            {
                return await rolRepositorio.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los roles xd");
            }
        }



  
    }
}
