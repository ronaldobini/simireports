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
                               "Protocol = onsoctcp; UID = informix; Password = xxx;";

            IfxConnection conn = new IfxConnection();
            conn.ConnectionString = connetionString;

            try
            {
                conn.Open();
                executar("set isolation to dirty read", conn);
            }
            catch (Exception ex)
            {
                conn = null;
                Console.WriteLine("ERRO AO CONECTAR COM BANCO logix" + ex);
            }
            return conn;

        }

        public string abrirErros()
        {
            string connetionString = null;
            string erros = "sem erros";
            connetionString = "Database = logix; Host = server.similar.ind.br; Server = ol_producao; Service = 9088;" +
                               "Protocol = olsoctcp; UID = informix; Password = AdXmX2006;";
        //olsoctcp 
        IfxConnection conn = new IfxConnection();
            conn.ConnectionString = connetionString;

            try
            {
                conn.Open();
                executar("set isolation to dirty read", conn);
            }
            catch (Exception ex)
            {
                conn = null;
                erros = "erro: "+ex;
            }
            return erros;

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

        public String consultarErros(string sql, IfxConnection conn)
        {
            string result = "Sem Erros";
            IfxDataReader reader = null;
            try
            {
                IfxCommand command = new IfxCommand(sql, conn);
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                result = "ERRO: " + e;
            }
            return result;
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
