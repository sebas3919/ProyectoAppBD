using AppBD.Modelos;
using AppBD.Repositorios;
using AppBD.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Controladores
{
    internal class UsersController
    {
        UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

        public async Task GuardarUsuario(Usuario usuario)
        {



            if (usuario == null || usuario.Name.Trim() == "")
            {
                throw new ArgumentException("El usuario no puede ser nulo o vacío");
            }
            await usuarioRepositorio.GuardarUsuario(usuario);
            GmailServicios emailServicios = new GmailServicios();
            var asunto = "Nuevo usuario registrado";
            var destinatario = usuario.Email;
            var mensaje = Properties.Resources.Config;
            mensaje = mensaje.Replace("[nombre]", usuario.Name);
            await emailServicios.EnviarEmail(destinatario, asunto, mensaje);
        }


        public async Task<List<Usuario>> Listar()
        {
            try
            {
                return await usuarioRepositorio.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los usuarios: " + ex.Message);
            }
        }

        
           
        

    }
}
