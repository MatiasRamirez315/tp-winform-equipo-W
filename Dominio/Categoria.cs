using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int IDCategoria { get; set; }

        public  string Descripcion { get; set; }

        public Categoria() { }
        public Categoria(int id, string descripcion) {
            IDCategoria = id;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
