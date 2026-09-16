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
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, \r\n       M.Id AS IdMarca, M.Descripcion AS Marca, \r\n       C.Id AS IdCategoria, C.Descripcion AS Categoria\r\nFROM ARTICULOS A\r\nLEFT JOIN MARCAS M ON A.IdMarca = M.Id\r\nLEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo auxArticulo = new Articulo();

                    auxArticulo.IDArticulo = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                        auxArticulo.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        auxArticulo.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        auxArticulo.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["Precio"] is DBNull))
                        auxArticulo.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        auxArticulo.Marca = new Marca();
                        auxArticulo.Marca.IDMarca = (int)datos.Lector["IdMarca"];
                        if (!(datos.Lector["Marca"] is DBNull))
                        {
                            auxArticulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                        }
                    }

                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        auxArticulo.Categoria = new Categoria();
                        auxArticulo.Categoria.IDCategoria = (int)datos.Lector["IdCategoria"];
                        if (!(datos.Lector["Categoria"] is DBNull))
                        {
                            auxArticulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                        }
                    }

                    lista.Add(auxArticulo);

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

        public void agregar(Articulo articulo, Imagen img)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) " +
                    "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS int);");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.IDMarca);
                datos.setearParametro("@IdCategoria", articulo.Categoria.IDCategoria);

                int nuevoIdArticulo = datos.ejecutarAccionScalar(); 

                datos.setearConsulta(
                    "INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");

                datos.setearParametro("@IdArticulo", nuevoIdArticulo);
                datos.setearParametro("@ImagenUrl", img.URLImagen);

                datos.EjecutarAccion();
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

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update Articulos set Codigo = @codigo , Nombre = @nombre, Descripcion = @descripcion, Precio = @precio , IdMarca = @idMarca ,IdCategoria = @idCategoria where Id = @Id ");
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.IDMarca);
                datos.setearParametro("@IdCategoria", articulo.Categoria.IDCategoria);
                datos.setearParametro("@Id", articulo.IDArticulo);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Articulos where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
