using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;
using WindowsFormsApp.negocio;

namespace WindowsFormsApp
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
            Text = "Agregar articulo";
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar articulo";

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Los campos no deben estar vacios.");
                    return;
                }

                try
                {
                    decimal precio = decimal.Parse(txtPrecio.Text);
                    articulo.Precio = precio;
                }
                catch (FormatException)
                {
                    MessageBox.Show("El campo Precio solo acepta numeros");
                    return;
                }

                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Categoria = (Categoria)comboBoxCategoria.SelectedItem;
                articulo.Marca = (Marca)comboBoxMarca.SelectedItem;

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente!");
                }
                else {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado Exitosamente!");
                }
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pboxAltaArt.Load(imagen);

            }
            catch (Exception)
            {

                pboxAltaArt.Load("https://imgs.search.brave.com/fVrzTsY8XbfClD6SD9ps0BmYFUEi7I2qsepvPy4Ypj4/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly90My5m/dGNkbi5uZXQvanBn/LzA3LzU2LzY3LzM0/LzM2MF9GXzc1NjY3/MzQ2Nl9RclpHNU45/bDM4TGw4cE1NQW5J/NzgwWWxQcVROMm5h/aC5qcGc");

            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                comboBoxCategoria.DataSource = categoriaNegocio.listar();
                comboBoxMarca.DataSource = marcaNegocio.listar();


                comboBoxCategoria.DisplayMember = "Descripcion";
                comboBoxCategoria.ValueMember = "Id";

                comboBoxMarca.DisplayMember = "Descripcion";
                comboBoxMarca.ValueMember = "Id";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo.ToString();
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    //txtImg.Text = articulo.Imagenes.ImagenUrl;
                    //cargarImagen(articulo.Imagenes.ImagenUrl);
                    comboBoxCategoria.SelectedValue = articulo.Categoria.Id;
                    comboBoxMarca.SelectedValue = articulo.Marca.Id;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {

        }
    }
}
