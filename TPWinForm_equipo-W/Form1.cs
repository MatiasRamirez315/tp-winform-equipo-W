using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_W
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dgvArticulos.DataSource = negocio.listar();

            if (dgvArticulos.Columns["Imagenes"] != null) {
                dgvArticulos.Columns["Imagenes"].Visible = false;
            }

            if (dgvArticulos.Columns["IDArticulo"] != null)
            {
                dgvArticulos.Columns["IDArticulo"].Visible =false;
            }
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
        }
    }
}
