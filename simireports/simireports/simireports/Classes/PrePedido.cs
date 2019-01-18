using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class PrePedido
    {

        private string codPed;
        private DateTime tempo;
        private string criador;
        private string repres;

        public string CodPed { get => codPed; set => codPed = value; }
        public DateTime Tempo { get => tempo; set => tempo = value; }
        public string Criador { get => criador; set => criador = value; }
        public string Repres { get => repres; set => repres = value; }

        public PrePedido(string codPed, DateTime tempo, string criador, string repres)
        {
            this.CodPed = codPed;
            this.Tempo = tempo;
            this.Criador = criador;
            this.Repres = repres;
        }

        public PrePedido()
        {

        }


     
       
    }
}