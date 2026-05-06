using AppBD.DAO;
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

        public async Task<List<UserListDAO>> Listar()
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

        public async Task EliminarUsuario(int id)
        {
            try
            {
                await usuarioRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }
        public async Task EditarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentException("El usuario no puede ser nulo.");

            if (usuario.Id <= 0)
                throw new ArgumentException("Debe especificar un ID válido para editar.");

            if (string.IsNullOrWhiteSpace(usuario.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException("La contraseña es obligatoria.");

            if (usuario.RoleId <= 0)
                throw new ArgumentException("Debe seleccionar un rol válido.");

            try
            {
                bool actualizado = await usuarioRepositorio.EditarUsuario(usuario);
                if (!actualizado)
                    throw new Exception("No se encontró el usuario para actualizar.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar usuario: " + ex.Message);
            }
        }
    }
}
    
