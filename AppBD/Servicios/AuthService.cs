using AppBD.Modelos;
using AppBD.Repositorios;
using System;
using System.Threading.Tasks;

namespace AppBD.Servicios
{
    public class AuthService
    {
        private UsuarioRepositorio repo = new UsuarioRepositorio();
        private GmailServicios gmail = new GmailServicios();

        public static string OTP;
        public static DateTime Expiration;
        public static Usuario CurrentUser;

        public async Task<Usuario> Login(string email, string password)
        {
            var user = await repo.ObtenerPorEmail(email);

            // (opcional) si tienes campo Activo:
            // if (user != null && !user.Activo) return null;

            if (user != null && user.Password == password)
            {
                CurrentUser = user;
                await GenerarYEnviarOTP(user.Email);
                return user;
            }

            return null;
        }

        private async Task GenerarYEnviarOTP(string email)
        {
            OTP = new Random().Next(100000, 999999).ToString();
            Expiration = DateTime.Now.AddMinutes(5);

            string asunto = "Código de acceso";
            string cuerpo = $"<h2>Tu código es:</h2><h1>{OTP}</h1><p>Expira en 5 minutos</p>";
            ;

            await gmail.EnviarEmail(email, asunto, cuerpo);
        }

        public bool ValidarOTP(string codigo)
        {
            return codigo == OTP && DateTime.Now <= Expiration;
        }
    }
}