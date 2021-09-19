using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string DescripcionMarca { get; set; }
        public Marca()
        {

        }
        public Marca(string nombre)
        {
            DescripcionMarca = nombre;
        }
        public Marca(int id, string nombre)
        {
            Id = id;
            DescripcionMarca = nombre;
        }


        public override string ToString()
        {
            return DescripcionMarca;
        }
    }
}
