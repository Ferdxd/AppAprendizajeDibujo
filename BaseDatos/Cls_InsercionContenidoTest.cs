using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.BaseDatos
{
    internal class Cls_InsercionContenidoTest
    {
        public void insertarPregunta(string pregunta)
        {
            ClsConexion clsConexion = new ClsConexion();
            try
            {
                clsConexion.mysqlManual("INSERT INTO tb_preguntas (texto_pregunta) VALUES ('" + pregunta + "');");
            }
            catch
            {

            }
        }

        public int buscarIdPregunta(string pregunta)
        {
            int id = 0;
            ClsConexion clsConexion = new ClsConexion();

            DataTable dt = clsConexion.consultaTablaDirecta("SELECT id_pregunta FROM tb_preguntas where texto_pregunta = '" + pregunta + "' Limit 1;");
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["id_pregunta"]);
            }
            return id;
        }


        public void insertarRespuesta(string respuesta, bool correcta, int id_pregunta)
        {
            ClsConexion clsConexion = new ClsConexion();
            try
            {
                int escorrecta = correcta ? 1 : 0;
                clsConexion.mysqlManual("INSERT INTO tb_respuestas (texto_respuesta, correcta, id_pregunta) VALUES ('" + respuesta + "', " + escorrecta + ", " + id_pregunta + ");");
            }
            catch
            {

            }
            
        }
        
        public void insertarEvaluaciones_Preguntas(int id_evaluacion, int id_pregunta)
        {
            ClsConexion clsConexion = new ClsConexion();
            try
            {
                clsConexion.mysqlManual("INSERT INTO tb_evaluaciones_preguntas (id_test, id_pregunta) VALUES ("+id_evaluacion+", "+id_pregunta+");");
            }
            catch
            {

            }
        }
    }
}
