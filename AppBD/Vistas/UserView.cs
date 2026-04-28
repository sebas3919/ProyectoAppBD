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
    public partial class UserView : Form
    {
        Usuario usuario;
        UsersController usersController;
            RolesController rolesController = new RolesController();    
        public UserView()
        {
            InitializeComponent();
            usuario = new Usuario();
            usersController = new UsersController();
            CargarComboBox();
            CargarDatosView();
        }

        async Task CargarComboBox()
        {
            List<Rol> roles = await rolesController.Listar();

            comboBox1.DataSource = null;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = roles;
           

        }

        async Task  CargarDatosView()
        {
            List<Usuario> usuarios = await usersController.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = usuarios;
        }

        private async void button1_Click(object sender, EventArgs e)
        {


            var usuario = new Usuario()
            {
                Name = textBox1.Text,
                Email = textBox2.Text,
                Password = textBox3.Text,
                RoleId = (int)comboBox1.SelectedValue
            };

            await usersController.GuardarUsuario(usuario);
            await CargarDatosView();

            MessageBox.Show("Usuario guardado exitosamente");
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
        }
    }

}
