using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class ReportBrady
    {
        private string cliente;
        private string codCliente;
        private Decimal valor;
        private string pedido;
        private string cidade;

        public ReportBrady(string cliente, string codCliente, decimal valor, string pedido, string cidade)
        {
            this.cliente = cliente;
            this.codCliente = codCliente;
            this.valor = valor;
            this.pedido = pedido;
            this.cidade = cidade;
        }

        public string Cliente { get => cliente; set => cliente = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public string Pedido { get => pedido; set => pedido = value; }
        public string Cidade { get => cidade; set => cidade = value; }
    }
}