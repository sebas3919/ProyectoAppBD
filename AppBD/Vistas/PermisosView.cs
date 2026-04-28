using AppBD.Controladores;
using AppBD.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBD.Vistas
{
    

    public partial class PermisosView : Form
    {
        Permisos permisos;
        PermisosController permisoscontroller;
        public PermisosView()
        {
            InitializeComponent();
            permisos = new Permisos();
            permisoscontroller = new PermisosController();
            CargarDatosView();

        }




        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            permisos.Name = permisosName.Text;
            await permisoscontroller.GuardarPermiso(permisos);
            _ = CargarDatosView();
            MessageBox.Show("Permiso guardado exitosamente");
        }

        async Task CargarDatosView()
        {
            List<Permisos> permisosList = await permisoscontroller.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = permisosList;

        }
    }
}

