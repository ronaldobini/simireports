using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Comissao
    {
        private string codEmpresa;
        private string numDocum;
        private string numDocumOrigem;
        private string numPedido;
        private string nomCliente;
        private Decimal valBruto;
        private Decimal pctComissao;
        private Decimal comiss;
        private string nomRepres;
        private DateTime datEmiss;
        private DateTime datVcto;
        private DateTime datPgto;
        
        private char iesPgtoDocum;

        public Comissao()
        {

        }

        public Comissao(string codEmpresa, string numDocum, string numDocumOrigem, string numPedido, string nomCliente, Decimal valBruto, Decimal pctComissao, Decimal comiss, string nomRepres, DateTime datEmiss, DateTime datVcto, DateTime datPgto, char iesPgtoDocum)
        {
            this.codEmpresa = codEmpresa;
            this.numDocum = numDocum;
            this.numDocumOrigem = numDocumOrigem;
            this.numPedido = numPedido;
            this.nomCliente = nomCliente;
            this.valBruto = valBruto;
            this.pctComissao = pctComissao;
            this.comiss = comiss;
            this.nomRepres = nomRepres;
            this.datEmiss = datEmiss;
            this.datVcto = datVcto;
            this.datPgto = datPgto;
            this.iesPgtoDocum = iesPgtoDocum;
        }

        public string CodEmpresa { get => codEmpresa; set => codEmpresa = value; }
        public string NumDocum { get => numDocum; set => numDocum = value; }
        public string NumDocumOrigem { get => numDocumOrigem; set => numDocumOrigem = value; }
        public string NumPedido { get => numPedido; set => numPedido = value; }
        public string NomCliente { get => nomCliente; set => nomCliente = value; }
        public Decimal ValBruto { get => valBruto; set => valBruto = value; }
        public Decimal PctComissao { get => pctComissao; set => pctComissao = value; }
        public Decimal Comiss { get => comiss; set => comiss = value; }
        public string NomRepres { get => nomRepres; set => nomRepres = value; }
        public DateTime DatEmiss { get => datEmiss; set => datEmiss = value; }
        public DateTime DatVcto { get => datVcto; set => datVcto = value; }
        public char IesPgtoDocum { get => iesPgtoDocum; set => iesPgtoDocum = value; }
        public DateTime DatPgto { get => datPgto; set => datPgto = value; }
    }
}