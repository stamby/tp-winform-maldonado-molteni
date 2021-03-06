using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public Marca DesdeDescripcion(string Descripcion)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.SetearConsulta("Select Id from Marcas where Descripcion = @Descripcion");
            datos.SetearParametros("@Descripcion", Descripcion);
            datos.EjecutarLectura();

            Marca aux = new Marca();

            if (datos.Lector.Read())
            {
                aux.Id = (int)datos.Lector["Id"];
                aux.DescripcionMarca = Descripcion;
                return aux;
            }
            else
                return null;
        }
        public Marca DesdeID(int ID)
        {
            AccesoDatos acceso = new AccesoDatos();
            acceso.SetearConsulta(
                "select descripcion from MARCAS where id = '" +
                ID + "';");
            acceso.EjecutarLectura();
            Marca marca = new Marca();
            marca.Id = ID;
            acceso.Lector.Read();
            marca.DescripcionMarca = (string)acceso.Lector["Descripcion"];
            acceso.CerrarConexion();

            return marca;
        }
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.SetearConsulta("select id, descripcion from MARCAS");
                acceso.EjecutarLectura();

                while (acceso.Lector.Read())
                {
                    lista.Add(new Marca((int)acceso.Lector["id"], (string)acceso.Lector["Descripcion"]));
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                acceso.CerrarConexion();
            }
        }

    }
}
