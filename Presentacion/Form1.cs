using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;
using System.Data.SqlClient;

namespace Presentacion
{
    public partial class Gestor : Form
    {
        private List<Articulo> listaArticulos;
        public Gestor()
        {
            InitializeComponent();

            pbArticulo.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarArticulos();
        }
        private void cargarArticulos()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            listaArticulos = articuloNegocio.ListarArticulo();
            dgvArticulos.DataSource = listaArticulos;
            RecargarImagen(listaArticulos[0].ImagenUrl);
        }
        private void RecargarImagen(string img)
        {
            try
            {
                pbArticulo.Load(img);
            }
            catch
            {
                pbArticulo.Load("404.png");
            }
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            formArticulo agregar = new formArticulo();
            agregar.ShowDialog();
            cargarArticulos();
        }

        private void dgvArticulos_MouseClick(object sender, MouseEventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            RecargarImagen(seleccionado.ImagenUrl);
        }

        private void bEditar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            if (seleccionado == null)
                return;

            formArticulo editar = new formArticulo(seleccionado);
            editar.ShowDialog();
            cargarArticulos();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string texto = txtFiltro.Text.ToUpper();

            if (txtFiltro.Text != "")
            {
                List<Articulo> listaFiltrada = listaArticulos.FindAll(
                    X => (
                        X.Nombre.ToUpper().Contains(texto) ||
                        X.Codigo.Contains(texto) ||
                        X.Descripcion.ToUpper().Contains(texto) ||
                        X.Marca.DescripcionMarca.ToUpper().Contains(texto) ||
                        X.Tipo.DescripcionCategoria.ToUpper().Contains(texto)));
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaFiltrada;
            }
            else
            {
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaArticulos;
            }
        }

        private void bEliminar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            DialogResult dialogResult = MessageBox.Show(
                "¿Desea eliminar el registro " +
                seleccionado.Codigo + " (\"" +
                seleccionado.Descripcion + "\")?",
                "Eliminar artículo",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                articuloNegocio.EliminarArticulo(seleccionado);
                cargarArticulos();
            }
        }

        private void bDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            formArticulo detalle = new formArticulo(seleccionado, true);
            detalle.ShowDialog();
        }
    }
}
