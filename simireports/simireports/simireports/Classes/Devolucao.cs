using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Devolucao
    {
        private String empresa;
        private String numDocum;
        private String avisoRec;
        private Decimal valor;
        private DateTime dataEmis;
        private String codItem;
        private Decimal preUnit;
        private String codOper;

        public Devolucao(string empresa, string numDocum, string avisoRec, decimal valor, DateTime dataEmis, string codItem, decimal preUnit, string codOper)
        {
            this.empresa = empresa;
            this.numDocum = numDocum;
            this.avisoRec = avisoRec;
            this.valor = valor;
            this.dataEmis = dataEmis;
            this.codItem = codItem;
            this.preUnit = preUnit;
            this.codOper = codOper;
        }

        public string Empresa { get => empresa; set => empresa = value; }
        public string NumDocum { get => numDocum; set => numDocum = value; }
        public string AvisoRec { get => avisoRec; set => avisoRec = value; }
        public decimal Valor { get => valor; set => valor = value; }
        public DateTime DataEmis { get => dataEmis; set => dataEmis = value; }
        public string CodItem { get => codItem; set => codItem = value; }
        public decimal PreUnit { get => preUnit; set => preUnit = value; }
        public string CodOper { get => codOper; set => codOper = value; }
    }
}