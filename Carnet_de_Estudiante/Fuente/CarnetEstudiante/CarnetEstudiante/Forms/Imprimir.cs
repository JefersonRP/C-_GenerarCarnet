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
using System.Threading.Tasks;

namespace CarnetEstudiante.Forms
{
    public partial class Imprimir : Form
    {

        

        public Imprimir(string cod, string nom, string pat, string mat, Image foto)
        {
            InitializeComponent();
            lblCodigo.Text = cod;
            lblNombre.Text = nom;
            lblPaterno.Text = pat;
            lblMaterno.Text = mat;
            picFoto.Image = foto;
        }


        private void imprimir()
        {
           
        }



        //No importante pero no borrar
        private void label1_Click(object sender, EventArgs e)
        {

        }

        //No importante pero no borrar
        private void label5_Click(object sender, EventArgs e)
        {

        }




        //METODO PARA DARLE CLICK A TODOS LOS ATRIBUTOS DEL WEB FORM E IMPRIMIR
        private void imagen_click(object sender, EventArgs e)
        {
            imprimir();

        }


        //CONTEO PARA LA IMPRESION
        private async void Imprimir_Load(object sender, EventArgs e)
        {
            await Task.Delay(2000);

            if (MessageBox.Show("¿Desea imprimir el carnet?", "Imprimir Carnet", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                Graphics gfx = this.CreateGraphics();

                Bitmap bmp = new Bitmap(this.Width, this.Height, gfx);

                this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                /*continuar aqui*/


                // Displays a SaveFileDialog so the user can save the Image
                // assigned to Button2.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.FileName = "" + lblPaterno.Text + "";
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog1.Title = "Guardar carnet de alumno";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                        (System.IO.FileStream)saveFileDialog1.OpenFile();
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            bmp.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            bmp.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            bmp.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }
                    this.Dispose();
                }

            }
            else
            {
                this.Dispose();
            }


        }
    }
}
