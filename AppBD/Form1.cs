using AppBD.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarOpciones();
        }

        public void CargarOpciones()
        {
            comboBox1.Items.Add("Usuarios");
            comboBox1.Items.Add("Roles");
            comboBox1.Items.Add("Permisos");

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Form vista = null;

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Usuarios":
                    vista = new UserView();
                    break;

                case "Roles":
                    vista = new RolesView();
                    break;
                    case "Permisos":
                    vista = new PermisosView();
                    break;
            }

            if (vista != null)
            {
                vista.TopLevel = false;
                vista.FormBorderStyle = FormBorderStyle.None;
                vista.Dock = DockStyle.Fill;

                panel1.Controls.Add(vista);
                panel1.Tag = vista;
                vista.Show();
            }
            
        }
    }
}
