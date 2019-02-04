using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class OMPendente
    {
        private string empresa;
        private DateTime datAltSit;
        private string codCliente;
        private string cliente;
        private string numPed;
        private string tipoEntrega;
        private List<Item> itens;

        public OMPendente(string empresa, DateTime datAltSit, string codCliente, string cliente, string numPed, string tipoEntrega,List<Item> itens)
        {
            this.empresa = empresa;
            this.datAltSit = datAltSit;
            this.codCliente = codCliente;
            this.cliente = cliente;
            this.numPed = numPed;
            this.tipoEntrega = tipoEntrega;
            this.itens = itens;
        }

        public string Empresa { get => empresa; set => empresa = value; }
        public DateTime DatAltSit { get => datAltSit; set => datAltSit = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string NumPed { get => numPed; set => numPed = value; }
        public string TipoEntrega { get => tipoEntrega; set => tipoEntrega = value; }
        public List<Item> Itens { get => itens; set => itens = value; }
        
    }
}