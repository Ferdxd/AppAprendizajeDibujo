using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AppDibujoArtistico.BaseDatos
{
    internal class Cls_tb_nivel1
    {

        public MySqlParameter ConvertirBytesAParametro(byte[] imagenData)
        {
            // Crea un parámetro con el nombre del parámetro y el tipo adecuado
            MySqlParameter parametro = new MySqlParameter("@imagenData", MySqlDbType.LongBlob);

            // Asigna el valor del parámetro como el arreglo de bytes
            parametro.Value = imagenData;

            return parametro;
        }

        public void insertarImagen(byte[] imagenData)
        {
            ClsConexion clsConexion = new ClsConexion();

            try
            {
                clsConexion.abrirConexion();
                string query = "INSERT INTO tb_nivel_1 (imagen_nvl1) VALUES (@imagenData)";
                MySqlCommand cmd = new MySqlCommand(query, clsConexion.ObtenerConexion());

                // Convierte los datos binarios en un parámetro compatible con LongBlob
                MySqlParameter parametro = ConvertirBytesAParametro(imagenData);
                cmd.Parameters.Add(parametro);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir durante la inserción
                int hola1 = 0;
            }
            finally
            {
                clsConexion.cerrarConexion();
            }
        }
        public void InsertarImagen(int idEvaluacion, byte[] imagenData, string descripcion)
        {
            ClsConexion clsConexion = new ClsConexion();

            try
            {
                clsConexion.abrirConexion();
                string query = "INSERT INTO tb_contenido_visual (id_evaluacion, imagen, descripcion) VALUES (@idEvaluacion, @imagenData, @descripcion)";
                MySqlCommand cmd = new MySqlCommand(query, clsConexion.ObtenerConexion());

                // Agrega los parámetros
                cmd.Parameters.AddWithValue("@idEvaluacion", idEvaluacion);
                MySqlParameter parametroImagen = ConvertirBytesAParametro(imagenData);
                cmd.Parameters.Add(parametroImagen);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir durante la inserción
                int hola1 = 0;
            }
            finally
            {
                clsConexion.cerrarConexion();
            }
        }




        public byte[] ConvertirImagenABlob(string rutaImagen)
        {
            byte[] imagenData = null;

            try
            {
                using (FileStream fs = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        imagenData = br.ReadBytes((int)fs.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al convertir la imagen a BLOB: " + ex.Message);
            }

            return imagenData;
        }

        public void cargarImagenes(List<byte[]> imagenes)
        {
            ClsConexion clsConexion=new ClsConexion();
           // List<byte[]> imagenes = new List<byte[]>();
            
            DataTable dt = clsConexion.consultaTablaDirecta("SELECT imagen_nvl1 FROM tb_nivel_1;");

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    byte[] imagenData = (byte[])row["imagen_nvl1"];
                    imagenes.Add(imagenData);

                }
            }
            catch
            {
                int hola = 0;
            }  

        }

        public Dictionary<string, List<Tuple<byte[], string>>>
 ObtenerContenidoVisualPorEvaluacion(string nombreEvaluacion)
        {
            ClsConexion clsConexion = new ClsConexion();
            Dictionary<string, List<Tuple<byte[], string>>> contenidoVisual =
                new Dictionary<string, List<Tuple<byte[], string>>>();

            try
            {
                clsConexion.abrirConexion();
                string query = "SELECT cv.imagen, cv.descripcion " +
                               "FROM tb_contenido_visual cv " +
                               "INNER JOIN tb_evaluaciones e ON cv.id_evaluacion = e.id_test " +
                               "WHERE e.nombre_test = @nombreEvaluacion";

                MySqlCommand cmd = new MySqlCommand(query, clsConexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@nombreEvaluacion", nombreEvaluacion);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] imagenData = (byte[])reader["imagen"];
                        string descripcion = reader["descripcion"].ToString();

                        // Si la clave aún no existe en el diccionario, agrégala
                        if (!contenidoVisual.ContainsKey(nombreEvaluacion))
                        {
                            contenidoVisual[nombreEvaluacion] = new List<Tuple<byte[], string>>();
                        }

                        contenidoVisual[nombreEvaluacion].Add(new Tuple<byte[], string>(imagenData, descripcion));
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir durante la consulta
            }
            finally
            {
                clsConexion.cerrarConexion();
            }

            return contenidoVisual;
        }


    }
}
