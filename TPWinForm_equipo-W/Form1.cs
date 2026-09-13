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
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();

            ImagenNegocio imagenNegocio = new ImagenNegocio();
            listaImagenes = imagenNegocio.listar();

            dgvArticulos.DataSource = listaArticulos;   

            if (dgvArticulos.Columns["Imagenes"] != null)
                dgvArticulos.Columns["Imagenes"].Visible = false;

            if (dgvArticulos.Columns["IDArticulo"] != null)
                dgvArticulos.Columns["IDArticulo"].Visible = false;
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null) return;
            if (listaImagenes == null || listaImagenes.Count == 0) return;

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            Imagen img = listaImagenes.FirstOrDefault(i => i.IDArticulo == seleccionado.IDArticulo);

            cargarImagen(img?.URLImagen);
        }

        private void cargarImagen(string imagen)
        {
            pbxImagen.WaitOnLoad = true;

            try
            {
                if (string.IsNullOrWhiteSpace(imagen))
                {
                    pbxImagen.Image = null;
                    return;
                }

                pbxImagen.Image = null;

                pbxImagen.Load(imagen.Trim());
            }
            catch
            {
                pbxImagen.Load("https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty.jpg");
            }
        }
    }
}
