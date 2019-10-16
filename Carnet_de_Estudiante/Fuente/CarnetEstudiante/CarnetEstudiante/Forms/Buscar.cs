using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarnetEstudiante.Forms
{
    public partial class Buscar : Form
    {
        private Clase.ClaseEstudiante estudiante = new Clase.ClaseEstudiante();
        public Buscar()
        {
            InitializeComponent();
        }

        private void Buscar_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }


        private void CargarGrilla()
        {
            dtgBuscar.DataSource = estudiante.Listar();
            dtgBuscar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgBuscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgBuscar.AllowUserToAddRows = false;
            dtgBuscar.AllowUserToDeleteRows = false;
            dtgBuscar.ReadOnly = true;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarXDnioCod();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Carnet formulario = new Carnet();
            formulario.Show();
            this.Hide();
        }

        //METODO PARA FILTRAR POR DNI O CODIGO
        private void ListarXDnioCod()
        {
            if (txtBuscar.Text == "") { CargarGrilla(); }
            else
            {
                dtgBuscar.DataSource = estudiante.ListarXDnioCod(txtBuscar.Text);
                dtgBuscar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtgBuscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dtgBuscar.AllowUserToAddRows = false;
                dtgBuscar.AllowUserToDeleteRows = false;
                dtgBuscar.ReadOnly = true;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
