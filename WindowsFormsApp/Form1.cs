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

namespace WindowsFormsApp

{
    public partial class Form1 : Form
    {

        private List<Articulo> listaArticulos;
        private List<Imagen> listaImagen;

        public Form1()
        {
            InitializeComponent();
            Articulo articulo = new Articulo();    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarArticulos();
        }
        private void cargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
          
                listaImagen = negocio.listarImagen();
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();

                int idArticulo = 0;
                if (listaArticulos.Count > 0) idArticulo = listaArticulos[0].Id;

                cargarImagen(traeUrlImagen(idArticulo), idArticulo);
                posicionImagen(idArticulo);

            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(dgvArticulos.CurrentRow != null)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    cargarImagen(traeUrlImagen(seleccionado.Id), seleccionado.Id);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cargarImagen(string imagen, int idArticulo, bool cargaDesdeCarrusel = false)
        {
            try
            {
                pbxArticulos.Load(imagen);
                if (!cargaDesdeCarrusel) posicionImagen(idArticulo);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load("https://imgs.search.brave.com/fVrzTsY8XbfClD6SD9ps0BmYFUEi7I2qsepvPy4Ypj4/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly90My5m/dGNkbi5uZXQvanBn/LzA3LzU2LzY3LzM0/LzM2MF9GXzc1NjY3/MzQ2Nl9RclpHNU45/bDM4TGw4cE1NQW5J/NzgwWWxQcVROMm5h/aC5qcGc");
            }
        }
        public string traeUrlImagen(int idArticulo, int posicion = 0)
        {
            string urlImg = "";

            if (listaImagen.Find(x => x.IdArticulo == idArticulo) != null)
            {
                List<Imagen> listaImgArtSel = new List<Imagen>();
                listaImgArtSel = listaImagen.FindAll(x => x.IdArticulo == idArticulo);
                urlImg = listaImgArtSel[posicion].ImagenUrl.ToString();

            }
            else
            {
                btnAtras.Enabled = false;
                btnSiguiente.Enabled = false;
            }

            return urlImg;
        }
        public int posicionImagen(int idArticulo)
        {
            int posicion = 0;
            if(idArticulo > 0)
            {
                if (EsPrimerImagen(idArticulo)) btnAtras.Enabled = false;
                else btnAtras.Enabled = true;

                if (EsUltimaImagen(idArticulo)) btnSiguiente.Enabled = false;
                else btnSiguiente.Enabled = true;

                if (!btnAtras.Enabled) return posicion;

                List<Imagen> imagenes = ordenaListaImagenById(idArticulo);
                foreach (Imagen img in imagenes)
                {
                    if (img.ImagenUrl == pbxArticulos.ImageLocation.ToString()) return posicion;
                    posicion++;
                }
            }

            return posicion;

        }
        public bool EsUltimaImagen(int idArticulo)
        {
            bool esUltimaImagen = false;
            int cantImg = 0;
            List<Imagen> imagenes = ordenaListaImagenById(idArticulo);
            cantImg = imagenes.Count;

            if (cantImg == 1 || imagenes[cantImg - 1].ImagenUrl == pbxArticulos.ImageLocation.ToString()) esUltimaImagen = true;

            return esUltimaImagen;
        }
        public List<Imagen> ordenaListaImagenById(int idArticulo)
        {
            List<Imagen> imagenes = new List<Imagen>();

            imagenes = listaImagen.FindAll(x => x.IdArticulo == idArticulo);
            imagenes = imagenes.OrderBy(x => x.Id).ToList();

            return imagenes;
        }
        public bool EsPrimerImagen(int idArticulo)
        {
            bool esPrimeraImagen = false;
            List<Imagen> imagenes = ordenaListaImagenById(idArticulo);

            if (imagenes.Count == 1 || imagenes[0].ImagenUrl == pbxArticulos.ImageLocation.ToString()) esPrimeraImagen = true;

            return esPrimeraImagen;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();   
            alta.ShowDialog();
            cargarArticulos();
        }
        

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;

            string filtro = txtFiltro.Text;

            if(filtro.Length >= 3)
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();

        }
        private void ocultarColumnas()
        {
            //dgvArticulos.Columns["Imagenes"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargarArticulos();
        }

        private void txtAtras_Click(object sender, EventArgs e)
        {

        }

        private void txtSiguiente_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (listaImagen.Find(x => x.IdArticulo == seleccionado.Id) != null)
                {
                    List<Imagen> imagenes = ordenaListaImagenById(seleccionado.Id);
                    int posicion = posicionImagen(seleccionado.Id);

                    if (posicion < imagenes.Count - 1)
                    {
                        posicion++;
                        if(!btnAtras.Enabled) btnAtras.Enabled = true;
                    }
                    if (posicion == imagenes.Count - 1 && btnSiguiente.Enabled) btnSiguiente.Enabled = false;

                    cargarImagen(traeUrlImagen(seleccionado.Id, posicion), seleccionado.Id, true);


                }
                else //si no tiene imagen valida, desabilito los botones siguiente y anterior
                {
                    btnAtras.Enabled = true;
                    btnSiguiente.Enabled = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al cargar imagen siguiente...");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();

            try
            {

                DialogResult respuesta = MessageBox.Show("¿Está seguro que Desea eliminar el artículo?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    negocio.eliminar(articulo.Id);

                    cargarArticulos();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al eliminar...");
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (listaImagen.Find(x => x.IdArticulo == seleccionado.Id) != null)
                {
                    int posicion = posicionImagen(seleccionado.Id);

                    if (posicion > 0)
                    {// si la posición es > 0 se va a mover hacia atras, entonces habilito el btnSiguiente, si es que está deshabilitado, porque ya no estaría en la última posición.
                        posicion = posicion - 1;
                        if (!btnSiguiente.Enabled) btnSiguiente.Enabled = true;
                    }
                    if (posicion == 0 && btnAtras.Enabled) btnAtras.Enabled = false;

                    cargarImagen(traeUrlImagen(seleccionado.Id, posicion), seleccionado.Id, true);

                }
                else //si no tiene imagen valida, desabilito los botones siguiente y anterior
                {
                    btnAtras.Enabled = true;
                    btnSiguiente.Enabled = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al cargar imagen previa...");
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (listaImagen.Find(x => x.IdArticulo == seleccionado.Id) != null)
                {
                    List<Imagen> imagenes = ordenaListaImagenById(seleccionado.Id);
                    int posicion = posicionImagen(seleccionado.Id);

                    if (posicion < imagenes.Count - 1)
                    {
                        posicion++;
                        if (!btnAtras.Enabled) btnAtras.Enabled = true;
                    }
                    if (posicion == imagenes.Count - 1 && btnSiguiente.Enabled) btnSiguiente.Enabled = false;

                    cargarImagen(traeUrlImagen(seleccionado.Id, posicion), seleccionado.Id, true);


                }
                else //si no tiene imagen valida, desabilito los botones siguiente y anterior
                {
                    btnAtras.Enabled = true;
                    btnSiguiente.Enabled = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al cargar imagen siguiente...");
            }
        }
    }
}
