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
using AppBD.Vistas;

namespace AppBD.Vistas
{
    public partial class inicioVista : Form
    {
        public inicioVista()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistroVista registroVista = new RegistroVista();
            registroVista.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AuthService auth = new AuthService();

                var user = await auth.Login(textBox1.Text.Trim(), textBox2.Text.Trim());

                if (user != null)
                {
                    MessageBox.Show("Se envió un código a tu correo");

                    OtpVista otp = new OtpVista();
                    otp.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
