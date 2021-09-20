using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public Categoria DesdeDescripcion(string Descripcion)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.SetearConsulta("Select Id from Categorias where Descripcion = @Descripcion");
            datos.SetearParametros("@Descripcion", Descripcion);
            datos.EjecutarLectura();

            Categoria aux = new Categoria();

            if (datos.Lector.Read())
            {
                aux.Id = (int)datos.Lector["Id"];
                aux.DescripcionCategoria = Descripcion;
                return aux;
            }
            else
                return null;
        }
        public List<Categoria> ListarCategoria()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select Id, descripcion from categorias");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.DescripcionCategoria = (string)datos.Lector["Descripcion"];

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
    }
}
