using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar()
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT I.Id, I.IdArticulo, I.ImagenUrl FROM IMAGENES I");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagen auxImagen = new Imagen();
                    auxImagen.IDImagen = (int)datos.Lector["Id"];
                    if (!(datos.Lector["IdArticulo"] is DBNull))
                    {
                        auxImagen.IDArticulo = (int)datos.Lector["IdArticulo"];
                    }
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        auxImagen.URLImagen = (string)datos.Lector["ImagenUrl"];
                    lista.Add(auxImagen);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
