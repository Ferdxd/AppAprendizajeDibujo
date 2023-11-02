using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.Archivos
{
    internal class ClsManipulacionImagenes
    {
        public List<string> ListarImagenesEnCarpeta(string carpetaRuta)
        {
            List<string> imagenes = new List<string>();

            try
            {
                // Verificar si la carpeta existe
                if (Directory.Exists(carpetaRuta))
                {
                    // Enumerar todos los archivos en la carpeta
                    string[] archivos = Directory.GetFiles(carpetaRuta);

                    // Filtrar archivos válidos y ordenar numéricamente por nombres de archivo
                    var archivosOrdenados = archivos
                        .Where(archivo => EsExtensionDeImagenValida(Path.GetExtension(archivo).ToLower()))
                        .OrderBy(archivo => int.Parse(Path.GetFileNameWithoutExtension(archivo)))
                        .Select(archivo => Path.GetFullPath(archivo))
                        .ToList();

                    imagenes.AddRange(archivosOrdenados);
                }
                else
                {
                    Console.WriteLine("La carpeta no existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return imagenes;
        }

        static bool EsExtensionDeImagenValida(string extension)
        {
            // Lista de extensiones de imagen válidas (puedes agregar más si es necesario)
            string[] extensionesValidas = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };

            return Array.Exists(extensionesValidas, ext => ext == extension);
        }
    }
}
