using AppBD.Modelos;
using AppBD.Repositorios;
using AppBD.Servicios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppBD.Controladores
{
    internal class UsersController
    {
        private UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

        public async Task GuardarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentException("El usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException("La contraseña es obligatoria.");

            if (usuario.RoleId <= 0)
                throw new ArgumentException("Debe seleccionar un rol válido.");

          
            await usuarioRepositorio.GuardarUsuario(usuario);

            
            GmailServicios emailServicios = new GmailServicios();

            string asunto = "Registro exitoso";
            string destinatario = usuario.Email;

            string mensaje = Properties.Resources.Registro_txt;
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
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
        }
    }
}