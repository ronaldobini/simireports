﻿using IBM.Data.Informix;
using simireports.simireports.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simireports.simireports
{
    public partial class RelResumoPedido : System.Web.UI.Page
    {

        public string postPedLogix = "";
        public string postPedCRM = "";

        public string resultado1 = "";
        public string resultado2 = "";

        public string resultado3 = "*Logo serão adicionados mais detalhes do pedido buscado integrando CRM e Logix.";

        public string resultado4 = "";
        public string resultado5 = "";
        public string resultado6 = "";
        public string resultado7 = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void buscar_Click(object sender, EventArgs e)
        {
            resultado1 = "Ped CRM: ";
            resultado2 = "Ped Logix: ";
            postPedCRM = pedCRM.Value;
            postPedLogix = pedLogix.Value;

            SqlConnection conn = new BancoAzure().abrir();
            SqlConnection conn2 = new BancoAzure().abrir();
            SqlConnection conn3 = new BancoAzure().abrir();

            string resultLog = Metodos.inserirLog((int)Session["idd"], "Executou Rel Resumo Ped", (string)Session["nome"], postPedLogix + " | "+postPedCRM);


            if (postPedLogix.Length > 0)
            {
                postPedLogix = postPedLogix.Trim();
                string sql = "SELECT codped FROM prepedidositens WHERE lgxpednum = "+postPedLogix+"";
                SqlDataReader reader = new BancoAzure().consultar(sql, conn);

                if (reader.Read())
                {
                    postPedCRM = reader.GetString(0).Trim();
                }
                else
                {
                    resultado1 = "Pedido CRM correspondente não encontrado";
                    resultado2 = postPedLogix;
                }

            }

            if (postPedCRM.Length > 0)
            {
                postPedCRM = postPedCRM.Trim();
                Metodos m = new Metodos();
                postPedCRM = m.configCoringasCodPedCRM(postPedCRM);
                string sql = "SELECT lgxpednum FROM prepedidositens WHERE codped like '" + postPedCRM + "' GROUP BY lgxpednum";
                SqlDataReader reader2 = new BancoAzure().consultar(sql, conn2);
                int i = 0;
                resultado1 = resultado1 + postPedCRM;
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        if (i > 0)
                        {
                            resultado2 = resultado2 + ", ";
                        }
                        resultado2 = resultado2 + reader2.GetInt32(0);
                        i++;
                    }

                    //outras info
                    string sql3 = "SELECT nom_cliente, Title, num_pedido_cli, Representante FROM PrePedidos WHERE codped like '" + postPedCRM + "'";
                    SqlDataReader reader3 = new BancoAzure().consultar(sql3, conn3);
                    if (reader3.HasRows)
                    {
                        reader3.Read();
                        resultado4 = reader3.GetString(0);
                        resultado5 = reader3.GetString(1);
                        resultado6 = reader3.GetString(2);
                        resultado7 = reader3.GetString(3);
                    }
                    else
                    {
                        resultado2 = "Pedido Logix correspondente não encontrado";
                    }
                }


                if (postPedCRM.Length <= 0 && postPedLogix.Length <= 0)
                {
                    resultado1 = "Preencha um dos campos por favor";
                    resultado2 = "";
                }
            }


            new BancoAzure().fechar(conn);
            new BancoAzure().fechar(conn2);
            new BancoAzure().fechar(conn3);

            }






        }
}