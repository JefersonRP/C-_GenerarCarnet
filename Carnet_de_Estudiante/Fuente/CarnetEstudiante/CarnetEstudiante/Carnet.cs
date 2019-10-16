using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarnetEstudiante
{
    public partial class Carnet : Form
    {
        public Carnet()
        {
            InitializeComponent();
        }




        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Forms.Agregar formulario = new Forms.Agregar();
            formulario.Show();
            this.Hide();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Forms.Buscar formulario = new Forms.Buscar();
            formulario.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
