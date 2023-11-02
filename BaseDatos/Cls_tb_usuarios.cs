using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppDibujoArtistico.BaseDatos
{
    internal class Cls_tb_usuarios
    {
        public void crearUsuario(string nombre, string apellido, string correo, string contraseña, string token)
        {
            ClsConexion clsConexion = new ClsConexion();
            clsConexion.mysqlManual("INSERT INTO tb_usuarios (nombre, apellido, correo, `contraseña`, token) VALUES ('"+nombre+"', '"+apellido+"', '"+correo+"', '"+contraseña+"', '"+token+"');");
        }

        public int encontrarAlumnoMail(string correo)
        {
            int id=0;
            ClsConexion clsConexion = new ClsConexion();

            DataTable dt = clsConexion.consultaTablaDirecta("SELECT id_usuario FROM tb_usuarios where correo = '"+correo+"' Limit 1;");
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["id_usuario"]);
            }
           
            return id;
        }

        public List<string> Obtener_Fila(string correo)
        {
            ClsConexion clsConexion = new ClsConexion();
            List<string> datos=new List<string>();
            DataTable dt = clsConexion.consultaTablaDirecta("SELECT nombre, apellido, correo, contraseña, token FROM tb_usuarios WHERE correo = '"+correo+"' Limit 1 ;");
            if (dt.Rows.Count > 0)
            {
                datos.Add(dt.Rows[0]["nombre"].ToString());
                datos.Add(dt.Rows[0]["apellido"].ToString());
                datos.Add(dt.Rows[0]["correo"].ToString());
                datos.Add(dt.Rows[0]["contraseña"].ToString());
                datos.Add(dt.Rows[0]["token"].ToString());
            }
            return datos;
        }

        public bool verificacionToken(int id_usuario, string tk, string tk2)
        {
            ClsConexion clsConexion = new ClsConexion();
            bool valorR = false;
            if (tk==tk2)
            {
                clsConexion.mysqlManual("UPDATE tb_usuarios SET token = 'Verificado' WHERE id_usuario=" + id_usuario + ";");
                valorR = true;
            }

            return valorR;
           
        }


    }
}
