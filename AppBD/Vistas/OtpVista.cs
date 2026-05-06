using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBD.Servicios;

namespace AppBD.Vistas
{
    public partial class OtpVista : Form
    {
        public OtpVista()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthService auth = new AuthService();

            if (auth.ValidarOTP(txtCodigo.Text.Trim()))
            {
                MessageBox.Show("Bienvenido " + AuthService.CurrentUser.Name);

                Form1 menu = new Form1();
                menu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Código incorrecto o expirado");
            }
        }
    }
}
