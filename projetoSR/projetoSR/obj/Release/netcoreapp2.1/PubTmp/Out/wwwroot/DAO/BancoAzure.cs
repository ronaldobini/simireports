using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoSR.DAO
{
    public class BancoAzure
    {

        public SqlConnection conectar()
        {
            string connetionString = null;
            SqlConnection conn;
            connetionString = "Data Source=similar.database.windows.net;Initial Catalog=SimiSysDB;User ID=similar;Password=@lbertor!ter15";
            conn = new SqlConnection(connetionString);
            try
            {
                conn.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO AO CONECTAR COM BANCO AZURE"+ex);
            }
            return conn;

        }



        public String retornarNome()
        {
            SqlConnection conn = conectar();
            string resultUsuarios = "-";
            SqlCommand command = new SqlCommand("SELECT * FROM Usuarios", conn);
            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.Read())
            {                
                resultUsuarios = (string) reader["nome"];
            }
            

            return resultUsuarios;
        }

        


    }
}
