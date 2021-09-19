using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListarArticulo()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select art.Id, art.Codigo, art.Nombre, art.Descripcion, mar.Descripcion as Marca , cat.Descripcion as Tipo, art.ImagenUrl, art.Precio, art.IdMarca, art.IdCategoria, art.Id from ARTICULOS as art inner join MARCAS as mar on art.IdCategoria = mar.Id inner join CATEGORIAS as cat on cat.Id = art.IdCategoria");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.DescripcionMarca = (string)datos.Lector["Marca"];

                    aux.Tipo = new Categoria();
                    aux.Tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.Tipo.DescripcionCategoria = (string)datos.Lector["Tipo"];

                    if(!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }
                
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void AgregarArticulo(Articulo AgregarArt)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Articulos (Codigo, Nombre, Descripcion, idMarca, IdCategoria, ImagenUrl, Precio) values ('" + AgregarArt.Codigo +"','"+ AgregarArt.Nombre + "','" + AgregarArt.Descripcion + "', @IdMarca, @IdCategoria, @ImagenUrl,'" + AgregarArt.Precio + "')");
                datos.SetearParametros("@Id", AgregarArt.Id);
                datos.SetearParametros("@Codigo", AgregarArt.Codigo);
                datos.SetearParametros("@Nombre", AgregarArt.Nombre);
                datos.SetearParametros("@Descripcion", AgregarArt.Descripcion);
                datos.SetearParametros("@IdMarca", AgregarArt.Marca.Id);
                datos.SetearParametros("@IdCategoria", AgregarArt.Tipo.Id);
                datos.SetearParametros("@Precio", AgregarArt.Precio);
                datos.SetearParametros("@ImagenUrl", AgregarArt.ImagenUrl);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void EditarArticulo(Articulo ModificarArt)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where id = @Id");
                datos.SetearParametros("@Codigo", ModificarArt.Codigo);
                datos.SetearParametros("@Nombre", ModificarArt.Nombre);
                datos.SetearParametros("@Descripcion", ModificarArt.Descripcion);
                datos.SetearParametros("@IdMarca", ModificarArt.Marca.Id);
                datos.SetearParametros("@IdCategoria", ModificarArt.Tipo.Id);
                datos.SetearParametros("@ImagenUrl", ModificarArt.ImagenUrl);
                datos.SetearParametros("@Precio", ModificarArt.Precio);
                datos.SetearParametros("@Id", ModificarArt.Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void EliminarArticulo(Articulo EliminarArt)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.SetearConsulta("delete from Articulos where id = @Id");
            datos.SetearParametros("@Id", EliminarArt.Id);
            datos.EjecutarAccion();
        }
    }
}
