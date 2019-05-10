using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Faturamento
    {
        private DateTime data;
        private string empresa;
        private string nota;
        private string clienteCPF;
        private string nomeCliente;
        private List<Item> itens;
        private string natureza;
        private string pedido;
        private string pedCli;
        private string trans;

        public Faturamento(DateTime data, string empresa, string nota, string clienteCPF, string nomeCliente, List<Item> itens, string natureza, string pedido, string pedCli, string trans)
        {
            this.data = data;
            this.empresa = empresa;
            this.nota = nota;
            this.clienteCPF = clienteCPF;
            this.nomeCliente = nomeCliente;
            this.itens = itens;
            this.natureza = natureza;
            this.pedido = pedido;
            this.pedCli = pedCli;
            this.trans = trans;
        }

        public DateTime Data { get => data; set => data = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public string Nota { get => nota; set => nota = value; }
        public string ClienteCPF { get => clienteCPF; set => clienteCPF = value; }
        public string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        public List<Item> Itens { get => itens; set => itens = value; }
        public string Natureza { get => natureza; set => natureza = value; }
        public string Pedido { get => pedido; set => pedido = value; }
        public string PedCli { get => pedCli; set => pedCli = value; }
        public string Trans { get => trans; set => trans = value; }
    }
}