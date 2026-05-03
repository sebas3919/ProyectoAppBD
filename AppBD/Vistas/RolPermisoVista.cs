using AppBD.Controladores;
using AppBD.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBD.Vistas
{
    public partial class RolPermisoVista : Form
    {
        List<Rol> roles;
        List<Permisos> permisos;

        RolesController rolesController;
        PermisosController permisosController;
        RolPermisoController rolPermisoController;

        public RolPermisoVista()
        {
            InitializeComponent();

            rolesController = new RolesController();
            permisosController = new PermisosController();
            rolPermisoController = new RolPermisoController();

            // Eventos
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;

            // Suscribir al evento Load del formulario en lugar de llamar a CargarDatos en el constructor
            this.Load += RolPermisoVista_Load;
        }

        private async void RolPermisoVista_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                roles = await rolesController.Listar();
                permisos = await permisosController.Listar();

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = null;

                // Validar que tengamos datos para evitar crear columnas vacías o nulas
                if (permisos == null || permisos.Count == 0)
                {
                    MessageBox.Show("No se encontraron permisos en la base de datos.");
                    return;
                }

                // 1. Añadir columnas
                dataGridView1.Columns.Add("RolId", "RolId"); // Mantendremos el ID visible temporalmente para depuración
                dataGridView1.Columns.Add("Rol", "Rol");

                foreach (var p in permisos)
                {
                    DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                    col.Name = p.Id.ToString();
                    col.HeaderText = p.Name;
                    dataGridView1.Columns.Add(col);
                }

                // 2. Añadir filas
                foreach (var rol in roles)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["RolId"].Value = rol.Id;
                    dataGridView1.Rows[rowIndex].Cells["Rol"].Value = rol.Name;

                    var permisosAsignados = await rolPermisoController.ObtenerPermisos(rol.Id);

                    foreach (var p in permisos)
                    {
                        bool tiene = permisosAsignados.Contains(p.Id);
                        dataGridView1.Rows[rowIndex].Cells[p.Id.ToString()].Value = tiene;
                    }
                }

                // Ocultar la columna del ID una vez terminada la carga
                dataGridView1.Columns["RolId"].Visible = false;
                dataGridView1.Columns["Rol"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que no estemos en la columna de texto ni en encabezados
            if (e.RowIndex < 0 || e.ColumnIndex <= 1) return;

            try
            {
                int rolId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RolId"].Value);
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                int permisoId = int.Parse(colName);
                bool activo = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[colName].Value);

                await rolPermisoController.TogglePermiso(rolId, permisoId, activo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el permiso: " + ex.Message);
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}