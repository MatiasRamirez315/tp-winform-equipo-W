using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int IDArticulo { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName ("Descripción")]
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; }
        public decimal Precio { get; set; }
      


        public override string ToString()
        {
            return Descripcion;
        }

        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
        //un producto podría llegar a tener una o más imágenes, sin un límite establecido. Esto debe estar contemplado en la gestión del artículo.


    }
}
