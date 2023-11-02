using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.BaseDatos
{
    class ClsConexion
    {
        private static MySqlConnection conexion;
        private String _conexion { get; }

        public ClsConexion()
        {
            _conexion = "Server=localhost;Port=3306;Database=db_appdibujo; Uid=root;Pwd=;";
            
        }



        public void abrirConexion()
        {
            
             conexion = new MySqlConnection(_conexion);
             conexion.Open();
            
        
        }

        public void cerrarConexion()
        {
            conexion.Close();
        }

       
        public void mysqlManual(String query)
        {
            abrirConexion();
            try
            {
                MySqlCommand comm = new MySqlCommand(query, conexion);
                comm.ExecuteReader();
            }catch(Exception ex)
            {
                
            }
            finally
            {
                cerrarConexion();
            }
        }

        public DataTable consultaTablaDirecta(String query)
        {
            abrirConexion();
            MySqlDataReader dr;
            MySqlCommand comm = new MySqlCommand(query, conexion);
            dr = comm.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(dr);

            cerrarConexion();
            return dataTable;
        }
        public MySqlConnection ObtenerConexion()
        {
            
            return conexion;
        }



        //"Server=localhost;Database=db_appdibujo; Uid=root;Pwd=;"


        //var conexion = new MySqlConnection(cadenaConexion);
        //conexion.Close();
    }
}
