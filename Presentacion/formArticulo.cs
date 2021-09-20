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

namespace Presentacion
{
    public partial class formArticulo : Form
    {
        private Articulo articulo = null;
        private bool detalle = false;
        public formArticulo()
        {
            InitializeComponent();

            Text = "Agregar Artículo";

            txtCodigo.Enabled = true;
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
        }
        public formArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }
        public formArticulo(Articulo articulo, bool detalle)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalle del Articulo " + articulo.Codigo;
            this.detalle = detalle;

        }
        private void formArticulo_Load(object sender, EventArgs e)
        {

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                comboBox1.DataSource = categoriaNegocio.ListarCategoria();
                cboMarca.DataSource = marcaNegocio.listar();

                if(articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDesc.Text = articulo.Descripcion;
                    cboMarca.SelectedIndex = cboMarca.FindStringExact(articulo.Marca.DescripcionMarca);
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(articulo.Tipo.DescripcionCategoria);
                    txtUrl.Text = articulo.ImagenUrl;
                    txtPrecio.Text = articulo.Precio.ToString("F2");
                    try
                    {
                        pbFormArt.Load(articulo.ImagenUrl);
                    }
                    catch
                    {
                        pbFormArt.Load("404.png");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error al cargar los artículos");
            }
            desBloquear();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            bool es_nuevo = articulo == null;

            if (es_nuevo)
            {
                // El artículo no existía antes: crear el objeto
                articulo = new Articulo();
            }

            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Debe ingresar un código de artículo.", "Error");
                return;
            }
            else
            {
                articulo.Codigo = txtCodigo.Text;
            }
            if (txtNombre.Text == "")
            {
                MessageBox.Show("El artículo precisa de un nombre.", "Error");
                return;
            }
            else
            {
                articulo.Nombre = txtNombre.Text;
            }

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            articulo.Marca = marcaNegocio.DesdeDescripcion(cboMarca.Text);
            if (articulo.Marca == null)
            {
                MessageBox.Show("Seleccione una marca de la lista desplegable.");
                return;
            }

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            articulo.Tipo = categoriaNegocio.DesdeDescripcion(comboBox1.Text);
            if (articulo.Tipo == null)
            {
                MessageBox.Show("Elegir la categoría del artículo.", "Error");
                return;
            }

            if (txtDesc.Text == "")
            {
                MessageBox.Show("Se necesita una descripción para el artículo.", "Error");
                return;
            }
            else
            {
                articulo.Descripcion = txtDesc.Text;
            }
            try
            {
                articulo.Precio = decimal.Parse(txtPrecio.Text);
            }
            catch
            {
                MessageBox.Show("El valor seleccionado como precio no es un número válido.", "Error");
                return;
            }

            articulo.ImagenUrl = txtUrl.Text;

            if (articulo.ImagenUrl == "")
            {
                MessageBox.Show("Se requiere el enlace a una imagen descriptiva del artículo.");
                return;
            }

            ArticuloNegocio artNegocio = new ArticuloNegocio();

            if (es_nuevo)
            {
                try
                {
                    artNegocio.AgregarArticulo(articulo);
                    MessageBox.Show("Se ha agregado el artículo al catálogo.", "Mensaje");
                }
                finally
                {
                    Close();
                }
            }
            else
            {
                try
                {
                    artNegocio.EditarArticulo(articulo);
                    MessageBox.Show("Se ha modificado el artículo en el catálogo.", "Mensaje");
                }
                finally
                {
                    Close();
                }
            }
        }
        private void desBloquear()
        {
            gbxArticulo.Enabled = !detalle;
            bAceptar.Visible = !detalle;
            bCancelar.Visible = !detalle;
        }
    }
}
