using System;
using System.Collections.Generic;
using System.Data.SqlClient;



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



        public SqlDataReader consultar(string sql, SqlConnection conn)
        {
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                
            }
            catch (Exception e)
            {
                string result = "ERRO: " + e;
            }
            return reader;
        }




        public string executar(string sql, SqlConnection conn)
        {
            string result = "-";
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
            return result;
        }
    public String consultarErros(string sql, SqlConnection conn)
    {
        string result = "Sem Erros";
        SqlDataReader reader = null;
        try
        {
            SqlCommand command = new SqlCommand(sql, conn);
            reader = command.ExecuteReader();
        }
        catch (Exception e)
        {
            result = "ERRO: " + e;
        }
        return result;
    }



}
