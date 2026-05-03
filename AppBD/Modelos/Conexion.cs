using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Modelos
{
    internal class Conexion
    {
        private string connectionString;

        public Conexion()
        {
            connectionString = ConfigurationManager.ConnectionStrings["miprimerabase"].ConnectionString;
        }

        public MySqlConnection GetConnectionString()
        {
            return new MySqlConnection(connectionString);
            
        }

    }
}
