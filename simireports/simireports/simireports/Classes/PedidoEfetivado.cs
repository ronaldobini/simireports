using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class PedidoEfetivado
    {
        private string codEmpresa;
        private DateTime dat;
        private string codCliente;
        private string numPed;
        private List<Item> itens;
        private string cliente;
        private string repres;
        private string pedCli;


        public PedidoEfetivado(string codEmpresa, DateTime dat, string codCliente, string numPed, List<Item> itens, string cliente, string repres)
        {
            this.codEmpresa = codEmpresa;
            this.dat = dat;
            this.codCliente = codCliente;
            this.numPed = numPed;
            this.itens = itens;
            this.cliente = cliente;
            this.repres = repres;
        }

        public PedidoEfetivado(string codEmpresa, DateTime dat, string codCliente, string numPed, List<Item> itens, string cliente, string repres, string pedCli)
        {
            this.codEmpresa = codEmpresa;
            this.dat = dat;
            this.codCliente = codCliente;
            this.numPed = numPed;
            this.itens = itens;
            this.cliente = cliente;
            this.repres = repres;
            this.pedCli = pedCli;
        }

        public string CodEmpresa { get => codEmpresa; set => codEmpresa = value; }
        public DateTime Dat { get => dat; set => dat = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public string NumPed { get => numPed; set => numPed = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Repres { get => repres; set => repres = value; }
        public List<Item> Itens { get => itens; set => itens = value; }
        public string PedCli { get => pedCli; set => pedCli = value; }
    }
}