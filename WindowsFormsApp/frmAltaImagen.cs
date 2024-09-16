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
        public frmAltaImagen()
        {
            InitializeComponent();
            Text = "Agregar Imágen";
        }
        public frmAltaImagen(Imagen img)
        {
            this.imagen = img;
            InitializeComponent();
            Text = "Modificar Imágen";
        }
        private void frmAltaImagen_Load(object sender, EventArgs e)
        {
            frmAltaArticulo altaArt = new frmAltaArticulo();

            string url = "";

            if (imagen == null) url = "";
            else
            {
                txtImagen.Text = imagen.ImagenUrl;
                url = imagen.ImagenUrl;
            }

            altaArt.cargarImagen(url);
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
                if(imagen == null) imagen = new Imagen();

                if(imagen.Id != 0)//modifico
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
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArt = new frmAltaArticulo();
            altaArt.cargarImagen(txtImagen.Text);
        }
    }
}
