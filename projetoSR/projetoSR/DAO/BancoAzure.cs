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
            //SqlCommand command = new SqlCommand("SELECT * FROM Usuarios", conn);
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                SqlCommand command = 
                    new SqlCommand("UPDATE Usuarios set pcom = 'PC_NA00', nlog = 1, configlist = 'Propostas|FUP|PrePedidos|Contatos|Clientes|Agenda|Produtos|FUPFast|MenuRelat|PrePedidosFUPFast|CdC|PriceCalc|Agenda_Short|ProdutosCentral' " +
                    "WHERE nome = 'Alyson'", conn);
                command.ExecuteNonQuery();
                resultUsuarios = "UPDATE DONE";
            }
            catch(Exception e)
            {
                resultUsuarios = resultUsuarios + e;
            }
            //if (reader.Read())
            //{
            //    resultUsuarios = (string)reader["nome"];
            //}


            return resultUsuarios;
        }

        


    }
}
