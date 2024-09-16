using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.negocio;

namespace WindowsFormsApp
{
    public partial class frmAltaImagen : Form
    {
        private Imagen imagen = null;
        private int idArticulo = 0;
        public frmAltaImagen(int idArticulo)
        {
            InitializeComponent();
            Text = "Agregar Imágen";
            this.idArticulo = idArticulo;
        }
        public frmAltaImagen(Imagen img)
        {
            this.imagen = img;
            InitializeComponent();
            Text = "Modificar Imágen";
        }
        private void frmAltaImagen_Load(object sender, EventArgs e)
        {

            string url = "";

            if (imagen == null) url = "";
            else
            {
                txtImagen.Text = imagen.ImagenUrl;
                url = imagen.ImagenUrl;
            }

            cargarImagen(url);
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);

            }
            catch (Exception)
            {

                pbxImagen.Load("https://imgs.search.brave.com/fVrzTsY8XbfClD6SD9ps0BmYFUEi7I2qsepvPy4Ypj4/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly90My5m/dGNkbi5uZXQvanBn/LzA3LzU2LzY3LzM0/LzM2MF9GXzc1NjY3/MzQ2Nl9RclpHNU45/bDM4TGw4cE1NQW5J/NzgwWWxQcVROMm5h/aC5qcGc");

            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImagen.Text))
            {
                MessageBox.Show("La url no debe estar vacia.");
                return;
            }

            try
            {
                ImagenNegocio negocio = new ImagenNegocio();
                if (imagen == null)
                {
                    imagen = new Imagen();
                    imagen.IdArticulo = idArticulo;
                }
                
                imagen.ImagenUrl = txtImagen.Text;
                
                if (imagen.Id != 0)//modifico
                {
                    negocio.modificar(imagen);
                    MessageBox.Show("Modificado Exitosamente!");
                }
                else//agrego
                {
                    negocio.agregar(imagen);
                    MessageBox.Show("Agregado Exitosamente!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ocurrió un error al agregar");
            }
            Close();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
    }
}
