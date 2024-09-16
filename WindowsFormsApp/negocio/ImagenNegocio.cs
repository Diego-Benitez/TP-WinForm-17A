using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.negocio
{
    public class ImagenNegocio
    {

        public void agregar(Imagen nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            Imagen imagen = new Imagen();
            try
            {
                datos.setearConsulta("insert into IMAGENES (IdArticulo,ImagenUrl) values(@idArticulo,@urlImagen)");
                datos.setearParametro("@idArticulo", nuevo.IdArticulo);
                datos.setearParametro("@urlImagen", nuevo.ImagenUrl);
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
                datos.setearConsulta("delete from IMAGENES where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificar(Imagen img)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update Imagenes set ImagenUrl = @urlImagen where id = @id");
                datos.setearParametro("@id", img.Id);
                datos.setearParametro("@urlImagen", img.ImagenUrl);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
