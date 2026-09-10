using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms; //habilita la creacion de los obj sql

namespace TPWinForm_equipo_W
{
    internal class ArticuloNegocio
    {
        public  List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection(); //conecta a bd
            SqlCommand comando = new SqlCommand(); //realiza acciones
            SqlDataReader lector;                                               //guardamos lo leido

            try
            {
                conexion.ConnectionString = "server=(localdb)\\MSSQLLocalDB; database=CATALOGO_P3_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, \r\n       M.Id AS IdMarca, M.Descripcion AS Marca, \r\n       C.Id AS IdCategoria, C.Descripcion AS Categoria\r\nFROM ARTICULOS A\r\nLEFT JOIN MARCAS M ON A.IdMarca = M.Id\r\nLEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id ";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read()) 
                {
                    Articulo auxArticulo = new Articulo();
                    Marca auxMarca = new Marca();
                    Categoria auxCategoria = new Categoria();

                    auxArticulo.IDArticulo = lector.GetInt32(0);
                    if (!lector.IsDBNull(1))
                        auxArticulo.Codigo = lector.GetString(1);
                    if (!lector.IsDBNull(2))
                        auxArticulo.Nombre = lector.GetString(2);
                    if (!lector.IsDBNull(3))
                        auxArticulo.Descripcion = lector.GetString(3);
                    if (!lector.IsDBNull(4))
                        auxArticulo.Precio = lector.GetDecimal(4);

                    if (!lector.IsDBNull(5))
                        auxMarca.IDMarca = lector.GetInt32(5);
                    if (!lector.IsDBNull(6))
                        auxMarca.Descripcion = lector.GetString(6);
                    auxArticulo.Marca = auxMarca;

                    if (!lector.IsDBNull(7))
                    {
                        int idCat = lector.GetInt32(7);
                        string descripCat = !lector.IsDBNull(8) ? lector.GetString(8) : "Sin Categoria";
                        auxCategoria.Descripcion = lector.GetString(8);
                        auxArticulo.Categoria = new Categoria(idCat, descripCat);
                    }
                    else
                    {
                        auxArticulo.Categoria = new Categoria(0, "Sin Categoria");
                    }
                    lista.Add(auxArticulo);
                    
                }

                return lista;
            }
            catch(SqlException ex) 
            {
                MessageBox.Show("erro ¡r " + ex);

                throw ex;
            }

        }
    }
}
