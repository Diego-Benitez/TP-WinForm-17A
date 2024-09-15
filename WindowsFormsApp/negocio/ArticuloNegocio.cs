using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> listArt = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("Select A.Id IdArticulo , A.Codigo, A.Nombre, M.Descripcion Marca,M.id IdMarca, C.Descripcion Categoria, C.Id IdCategoria, A.Precio, A.Descripcion, I.ImagenUrl  " +
                    "from ARTICULOS A " +
                    "INNER JOIN MARCAS M ON A.IdMarca = M.Id " +
                    "INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT " +
                    "JOIN IMAGENES I ON A.Id = I.IdArticulo");
                //datos.setearConsulta("SELECT Codigo, Nombre, m.Descripcion Marca, C.Descripcion Categoria, Precio, A.Descripcion, I.ImagenUrl  from ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I where A.idmarca = M.id and A.idcategoria = c.id and a.Id = i.IdArticulo");
                datos.ejecutarLectura();
            

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();

                    art.Id = (int)datos.Lector["IdArticulo"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Descripcion = (string)datos.Lector["descripcion"];

                    art.Marca = new Marca();
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Marca.Descripcion = (string)datos.Lector["marca"];

                    art.Categoria = new Categoria();
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    art.Precio = (Decimal)datos.Lector["Precio"];

                    art.Imagenes = new Imagen();
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.Imagenes.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    listArt.Add(art);
                }

                return listArt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            Imagen imagen = new Imagen();
            try
            {
                //  datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre,Descripcion,Precio,IdMarca,IdCategoria)VALUES(@Codigo, @Nombre, @Descripcion,@Precio, @IdMarca, @IdCategoria)");
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria)VALUES('"+ nuevo.Codigo + "','" + nuevo.Nombre + "','" + nuevo.Descripcion + "'," + nuevo.Precio +", "+ nuevo.Marca.Id +"," + nuevo.Categoria.Id+")");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.ejecutarAccion();

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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from articulos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificar(Articulo art)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update articulos set codigo = @codigo,nombre = @nombre,descripcion = @descripcion,idmarca = @idmarca, idcategoria = @idcategoria,precio = @precio where id = @id");
                datos.setearParametro("@id", art.Id);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@codigo", art.Codigo);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@idmarca", art.Marca.Id);
                datos.setearParametro("@idcategoria", art.Categoria.Id);
                datos.setearParametro("@precio", art.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
