using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class OrdemCompra
    {
        private string numOC;
        private string codItem;
        private string empresa;
        private DateTime previstaChegada;
        private int qtd;
        private string numDocum;

        public OrdemCompra(string numOC, string codItem, string empresa, DateTime previstaChegada, int qtd, string numDocum)
        {
            this.empresa = empresa;
            this.numOC = numOC;
            this.codItem = codItem;
            this.previstaChegada = previstaChegada;
            this.qtd = qtd;
            this.numDocum = numDocum;
        }

        public string CodItem { get => codItem; set => codItem = value; }
        public DateTime PrevistaChegada { get => previstaChegada; set => previstaChegada = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public string NumDocum { get => numDocum; set => numDocum = value; }
        public string NumOc { get => numOC; set => numOC = value; }
        public string Empresa { get => empresa; set => empresa = value; }
    }
}