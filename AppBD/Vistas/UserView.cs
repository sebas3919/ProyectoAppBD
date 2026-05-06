using AppBD.Controladores;
using AppBD.DAO;
using AppBD.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBD.Vistas
{
    public partial class UserView : Form
    {
        Usuario usuario;
        private int? usuarioEditandoId = null;
        UsersController usersController;
        RolesController rolesController = new RolesController();
        List<UserListDAO> usuarios;

        public UserView()
        {
            InitializeComponent();
            usuario = new Usuario();
            usersController = new UsersController();
            usuarios = new List<UserListDAO>();
            _=CargarComboBox();
            _=CargarDatosView();
        }

        async Task CargarComboBox()
        {
            List<Rol> roles = await rolesController.Listar();

            comboBox1.DataSource = null;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = roles;
        }

        async Task CargarDatosView()
        {
            // Actualiza la lista global, no una local
            usuarios = await usersController.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = usuarios;

            // Evita duplicar columnas de botones
            if (!dataGridView1.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.HeaderText = "Editar";
                btnEdit.Text = "Editar";
                btnEdit.Name = "btnEdit";
                btnEdit.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnEdit);
            }

            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "Eliminar";
                btnDelete.Text = "Eliminar";
                btnDelete.Name = "btnDelete";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnDelete);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario()
            {
                Id = usuarioEditandoId ?? 0, // si es edición, tendrá valor
                Name = textBox1.Text,
                Email = textBox2.Text,
                Password = textBox3.Text,
                RoleId = (int)comboBox1.SelectedValue
            };

            if (usuarioEditandoId == null)
            {
                // Crear nuevo
                await usersController.GuardarUsuario(usuario);
                MessageBox.Show("Usuario guardado exitosamente");
            }
            else
            {
                // Editar existente
                await usersController.EditarUsuario(usuario);
                MessageBox.Show("Usuario editado exitosamente");
                usuarioEditandoId = null; // resetear
            }

            await CargarDatosView();
            LimpiarCampos();
        }


        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private async void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var usuarioSeleccionado = usuarios[e.RowIndex];

                if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                   
                    usuarioEditandoId = usuarioSeleccionado.Id;

                    // Cargar datos en los TextBox
                    textBox1.Text = usuarioSeleccionado.UserName;
                    textBox2.Text = usuarioSeleccionado.Email;
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(usuarioSeleccionado.Rolename);

                    MessageBox.Show("Usuario cargado para edición. Modifique los campos y presione Guardar.");
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    await usersController.EliminarUsuario(usuarioSeleccionado.Id);
                    await CargarDatosView();
                    MessageBox.Show("Usuario eliminado exitosamente");
                }
            }
        }

    }
}
