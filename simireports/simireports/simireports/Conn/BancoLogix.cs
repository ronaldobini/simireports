using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using IBM.Data.Informix;

public class BancoLogix
    {

        public IfxConnection abrir()
        {
            string connetionString = null;
            
            connetionString = "Database = logix; Host = server.similar.ind.br; Server = ol_producao; Service = 9088;" +
                               "Protocol = onsoctcp; UID = informix; Password = AdXmX2006;";

            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = connetionString;

        try
            {
                conn.Open();

            }
            catch (Exception ex)
            {
                conn = null;
                Console.WriteLine("ERRO AO CONECTAR COM BANCO AZURE" + ex);
            }
            return conn;

        }


        public void fechar(IfxConnection conn)
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



        public IfxDataReader consultar(string sql, IfxConnection conn)
        {
        IfxDataReader reader = null;
            try
            {
                IfxCommand command = new IfxCommand(sql, conn);
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                string result = "ERRO: " + e;
            }
            return reader;
        }




        public string executar(string sql, IfxConnection conn)
        {
            string result = "-";
            try
            {
                IfxCommand command =
                    new IfxCommand(sql, conn);
                command.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception e)
            {
                result = "ERRO: " + e;
            }
            return result;
        }




    }
