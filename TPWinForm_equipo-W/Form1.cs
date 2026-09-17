using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace TPWinForm_equipo_W
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        private List<Imagen> listaImagenes;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
            cargar();
        }

        private void cargar()
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();

            ImagenNegocio imagenNegocio = new ImagenNegocio();
            listaImagenes = imagenNegocio.listar();

            dgvArticulos.DataSource = listaArticulos;

            OcultarColumnas();
        }

        private void OcultarColumnas()
        {

            if (dgvArticulos.Columns["Imagenes"] != null)
                dgvArticulos.Columns["Imagenes"].Visible = false;

            if (dgvArticulos.Columns["IDArticulo"] != null)
                dgvArticulos.Columns["IDArticulo"].Visible = false;

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            ImagenNegocio img = new ImagenNegocio();
            string url = img.listarImagenPorIDArticulo(seleccionado.IDArticulo);

            cargarImagen(url);

            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch
            {
                pbxImagen.Load("https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty.jpg");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo modificar;
            modificar = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAltaArticulo modificarArticulo = new frmAltaArticulo(modificar);
            modificarArticulo.ShowDialog();
            cargar();
           
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Se eliminara el articulo seleccionado", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                   seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                   articuloNegocio.eliminar(seleccionado.IDArticulo);
                   cargar();
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Articulo> filtrados;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                filtrados = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains( txtFiltro.Text.ToLower()) || x.Marca.Descripcion.ToLower().Contains(filtro.ToLower())); 
            }
            else
            {
                filtrados = listaArticulos;
            }


            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = filtrados;
            OcultarColumnas(); //oculta id y url
        }
    }
}
