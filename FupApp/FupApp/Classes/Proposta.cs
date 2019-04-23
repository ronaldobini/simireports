using System;
using System.Collections.Generic;
using System.Text;

namespace FupApp.Classes
{
    class Proposta
    {
        private string codProp;
        private string ufo;
        private string ufd;
        private string codCliente;
        private string nomeCliente;
        private string repres;

        public Proposta(string codProp, string ufo, string ufd, string codCliente, string nomeCliente, string repres)
        {
            this.codProp = codProp;
            this.ufo = ufo;
            this.ufd = ufd;
            this.codCliente = codCliente;
            this.nomeCliente = nomeCliente;
            this.repres = repres;
        }

        public string CodProp { get => codProp; set => codProp = value; }
        public string Ufo { get => ufo; set => ufo = value; }
        public string Ufd { get => ufd; set => ufd = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        public string Repres { get => repres; set => repres = value; }
    }
}
