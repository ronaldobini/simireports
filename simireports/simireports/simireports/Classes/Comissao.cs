using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Comissao
    {

        private string numPed;
        private string tempo;
        private string comiss;
        private string repres;
        private string codCliente;
        private string codEmpresa;

        public string NumPed { get => numPed; set => numPed = value; }
        public string Tempo { get => tempo; set => tempo = value; }
        public string Comiss { get => comiss; set => comiss = value; }
        public string Repres { get => repres; set => repres = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public string CodEmpresa { get => codEmpresa; set => codEmpresa = value; }

        public Comissao(string numPed, string tempo, string comiss, string repres, string codCliente, string codEmpresa)
        {
            this.NumPed = numPed;
            this.Tempo = tempo;
            this.Comiss = comiss;
            this.Repres = repres;
            this.CodCliente = codCliente;
            this.codEmpresa = codEmpresa;
        }

        public Comissao()
        {

        }


     
       
    }
}