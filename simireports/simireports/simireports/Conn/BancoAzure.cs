using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


    public class BancoAzure
    {

        public SqlConnection abrir()
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
                Console.WriteLine("ERRO AO CONECTAR COM BANCO AZURE" + ex);
            }
            return conn;

        }


        public void fechar(SqlConnection conn)
        {

            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO AO DAR CLOSE COM BANCO AZURE" + ex);
            }

        }



        public string consultar(string sql)
        {
            string result = "-";
            SqlConnection conn = abrir();
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            catch (Exception e)
            {
                result = "ERRO: " + e;
            }
            fechar(conn);
            return result;
        }




        public string executar(string sql)
        {
            string result = "-";
            SqlConnection conn = abrir();
            try
            {
                SqlCommand command =
                    new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception e)
            {
                result = "ERRO: " + e;
            }
            fechar(conn);
            return result;
        }




    }
