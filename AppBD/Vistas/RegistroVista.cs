using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBD.Controladores;
using AppBD.Modelos;
using AppBD.Repositorios;

namespace AppBD.Vistas
{
    public partial class RegistroVista : Form
    {
        private UsersController usersController;
        private RolRepositorio rolRepositorio;

        public RegistroVista()
        {
            InitializeComponent();

            usersController = new UsersController();
            rolRepositorio = new RolRepositorio();

            this.Load += RegistroVista_Load;
            button1.Click += button1_Click;
        }

        // Cargar roles desde la base de datos
        private async void RegistroVista_Load(object sender, EventArgs e)
        {
            try
            {
                var roles = await rolRepositorio.Listar();

                comboBox1.DataSource = roles;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Registrar usuario
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Name = textBox1.Text.Trim(),
                    Email = RegistroEmailtxt.Text.Trim(),
                    Password = textBox3.Text.Trim(),
                    RoleId = Convert.ToInt32(comboBox1.SelectedValue)
                };

                await usersController.GuardarUsuario(usuario);

                MessageBox.Show("Registro exitoso. Se ha enviado un correo de confirmación.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en registro: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            textBox1.Clear();
            RegistroEmailtxt.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }
    }
}