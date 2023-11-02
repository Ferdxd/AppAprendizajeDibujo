using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.BaseDatos
{
    internal class Cls_Evaluaciones
    {
      
      public Dictionary<string, List<Tuple<string, bool>>> cargarPreguntasYRespuestas(string nombreEvaluacion)
{
    ClsConexion clsConexion = new ClsConexion();
    Dictionary<string, List<Tuple<string, bool>>> preguntasYRespuestas = new Dictionary<string, List<Tuple<string, bool>>>();

    // Consulta modificada para filtrar por nombre de evaluación
    DataTable dtPreguntas = clsConexion.consultaTablaDirecta(
        "SELECT e.nombre_test AS Evaluacion, p.texto_pregunta AS Pregunta, r.texto_respuesta AS Respuesta, r.correcta AS EsCorrecta " +
        "FROM tb_evaluaciones e " +
        "JOIN tb_evaluaciones_preguntas ep ON e.id_test = ep.id_test " +
        "JOIN tb_preguntas p ON ep.id_pregunta = p.id_pregunta " +
        "JOIN tb_respuestas r ON p.id_pregunta = r.id_pregunta " +
        "WHERE e.nombre_test = '" + nombreEvaluacion + "';");

    foreach (DataRow preguntaRow in dtPreguntas.Rows)
    {
        string textoPregunta = preguntaRow["Pregunta"].ToString();
        string textoRespuesta = preguntaRow["Respuesta"].ToString();
        bool esCorrecta = Convert.ToBoolean(preguntaRow["EsCorrecta"]);

        if (!preguntasYRespuestas.ContainsKey(textoPregunta))
        {
            preguntasYRespuestas[textoPregunta] = new List<Tuple<string, bool>>();
        }

        preguntasYRespuestas[textoPregunta].Add(new Tuple<string, bool>(textoRespuesta, esCorrecta));
    }

    return preguntasYRespuestas;
}

        public void insertarNotaUbi(int id_usuario, int puntuacion)
        {
            ClsConexion clsconexion= new ClsConexion();
            clsconexion.mysqlManual("INSERT INTO tb_puntuaciones_ubicacion (id_usuario, puntuacion) VALUES ("+id_usuario+", "+puntuacion+");");
        }

        public int seleccionarNotaUbi(int id_usuario)
        {
            int nota = 0;
            ClsConexion clsConexion = new ClsConexion();

            DataTable dt = clsConexion.consultaTablaDirecta("SELECT puntuacion FROM tb_puntuaciones_ubicacion where id_usuario = '" + id_usuario + "' Limit 1;");
            if (dt.Rows.Count > 0)
            {
                nota = Convert.ToInt32(dt.Rows[0]["puntuacion"]);
            }
            return nota;
        }

        public void insertarNotaExamen(int id_usuario, int puntuacion, string tabla)
        {
            ClsConexion clsconexion = new ClsConexion();
            clsconexion.mysqlManual("INSERT INTO "+tabla+" (id_usuario, puntuacion) VALUES (" + id_usuario + ", " + puntuacion + ");");
        }

        public int seleccionarNotaExamen(int id_usuario, string tabla)
        {
            int nota = 0;
            ClsConexion clsConexion = new ClsConexion();

            DataTable dt = clsConexion.consultaTablaDirecta("SELECT puntuacion FROM "+tabla+" where id_usuario = '" + id_usuario + "' Limit 1;");
            if (dt.Rows.Count > 0)
            {
                nota = Convert.ToInt32(dt.Rows[0]["puntuacion"]);
            }
            return nota;
        }

        public void actualizarNotaExamen(int id_usuario,int puntuacion, string tabla)
        {
            ClsConexion clsConexion = new ClsConexion();
            clsConexion.mysqlManual("UPDATE "+tabla+" SET puntuacion = "+puntuacion+" WHERE id_puntuacion = "+id_usuario+";");
        }



    }
}
