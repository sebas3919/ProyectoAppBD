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
    public partial class RolesView : Form
    {

        Rol rol;
        RolesController rolesController; //= new RolesController();
        public RolesView()
        {
            InitializeComponent();
            rol = new Rol();
            rolesController = new RolesController();
            CargarDatosView();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            rol.Name = rolName.Text;
            await rolesController.GuardarRol(rol);
            _ = CargarDatosView();
            MessageBox.Show("Rol guardado exitosamente");
        }


        async Task CargarDatosView()
        {
            List<Rol> roles = await rolesController.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = roles; 

        }
    }

  

}

    





   

