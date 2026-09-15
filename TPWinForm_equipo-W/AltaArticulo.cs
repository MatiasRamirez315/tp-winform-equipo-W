using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPWinForm_equipo_W
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo modificar)
        {
            InitializeComponent();
            articulo = modificar;
            Text = "Modificar";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;   
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;


                if (articulo.IDArticulo != 0)
                {
                articuloNegocio.Modificar(articulo);
                MessageBox.Show("Articulo modificado correctamente");
                }
                else
                {
                articuloNegocio.agregar(articulo);
                MessageBox.Show("Articulo agregado correctamente");
                }


                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "IDCategoria";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "IDMarca";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString(); 
                    if (articulo.Marca != null)
                    {
                    cboMarca.SelectedValue = articulo.Marca.IDMarca;
                    }

                    if (articulo.Categoria != null)
                    {
                    cboCategoria.SelectedValue = articulo.Categoria.IDCategoria;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
