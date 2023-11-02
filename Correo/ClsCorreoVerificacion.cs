using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.Correo
{
    internal class ClsCorreoVerificacion
    {
       
        public string Token()
        {
            Random ran = new Random();
            string caracteres = "1234567890abcdefghijklmnñopqrstuvwxyz";

            string clave = "";
            for (int i = 0; i < 5; i++)
            {
                int aleatorio = ran.Next(caracteres.Length);
                clave += caracteres[aleatorio];
            }
            return clave;
        }
    }
}
