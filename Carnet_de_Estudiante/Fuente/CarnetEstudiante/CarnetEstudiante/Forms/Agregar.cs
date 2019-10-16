using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace CarnetEstudiante.Forms
{
    public partial class Agregar : Form
    {
        public OpenFileDialog examinar = new OpenFileDialog();
        private Clase.ClaseEstudiante estudiante = new Clase.ClaseEstudiante();


        public Agregar()
        {
            InitializeComponent();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {

            DialogResult dres1 = examinar.ShowDialog();
            if (dres1 == DialogResult.Abort)
                return;
            if (dres1 == DialogResult.Cancel)
                return;

            picFoto.Image = Image.FromFile(examinar.FileName);
        }
        //METODO PARA LIMPIAR
        private void limpiarForm()
        {
            txtCodigo.Clear();
            txtDNI.Clear();
            txtNombre.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            picFoto.Image = null;

        }

        //METODO PARA RESTRINGIR 
        //Solo dígitos
        private void soloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) { e.Handled = false; }
            else if (Char.IsControl(e.KeyChar)) { e.Handled = false; }
            else if (Char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else
            {
                e.Handled = true;
            }
        }
        //Solo letras
        private void soloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar)) { e.Handled = false; }
            else if (Char.IsControl(e.KeyChar)) { e.Handled = false; }
            else if (Char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else
            {
                e.Handled = true;
            }

        }

        //BOTON AGREGAR

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDNI.Text == "")
            {
                MessageBox.Show("Ingrese DNI");
            }
            else if (!Regex.IsMatch(txtDNI.Text, @"^\d{8}"))
            {
                MessageBox.Show("DNI debe tener 8 caracteres");
            }
            else if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese nombres");
            }
            else if (txtPaterno.Text == "")
            {
                MessageBox.Show("Ingrese apellido paterno");
            }
            else if (txtMaterno.Text == "")
            {
                MessageBox.Show("Ingrese apellido materno");
            }
            else
            {
                //Se inicailiza un flujo de archivo con la imagen seleccionada desde el disco.
                FileStream stream = new FileStream(examinar.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(stream);
                FileInfo fi = new FileInfo(examinar.FileName);
                //Se inicializa un arreglo de Bytes del tamaño de la imagen
                byte[] binData = new byte[stream.Length];
                //Se almacena en el arreglo de bytes la informacion que se obtiene del flujo de archivos(foto)
                //Lee el bloque de bytes del flujo y escribe los datos en un búfer dado.
                stream.Read(binData, 0, Convert.ToInt32(stream.Length));
                ////Se muetra la imagen obtenida desde el flujo de datos
                picFoto.Image = Image.FromStream(stream);

                txtNombre.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtNombre.Text).ToLower());
                txtPaterno.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPaterno.Text.ToLower());
                txtMaterno.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMaterno.Text.ToLower());


                if (txtCodigo.Text == "")
                {
                    if (MessageBox.Show("¿Continuar?", "Mensaje", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //Registro
                        string mensaje = estudiante.Registrar(txtCodigo.Text, txtDNI.Text.Trim(), txtNombre.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), binData);
                        MessageBox.Show(mensaje);
                    }

                    limpiarForm();
                }
            }
        }
    

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Carnet formulario = new Carnet();
            formulario.Show();
            this.Hide();
        }

        //BOTON DE IMPRIMIR CON CONDICIONES
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //POR SI OLVIDA LLENAR LOS CAMPOS ENVIAR MENSAJE
            if (txtNombre.Text != "" )
            {
                string cod = txtCodigo.Text;
            string nom = txtNombre.Text;
            string pat = txtPaterno.Text;
            string mat = txtMaterno.Text;
            Image foto = picFoto.Image;

            Imprimir imp = new Imprimir(cod, nom, pat, mat, foto);
                imp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Complete los campos");
            }
        }






        private void CargarCodigo()
        {
            txtCodigo.Text = estudiante.CodigoAutogenerar();

        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            CargarCodigo();
        }
    }
}
