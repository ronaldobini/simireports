using System;
using System.Collections.Generic;
using System.Text;

namespace FupApp.Classes
{
    class Estoque
    {
        private string codItem;
        private string desc;
        private string empresa;
        private int qtdLiberada;
        private int qtdReservada;
        private int qtdPed;
        private int qtdOc;
        private DateTime dataUltSP;

        public Estoque(string codItem, string empresa, int qtdLiberada, int qtdReservada, int qtdPed, int qtdOc, DateTime dataUltSP, string desc)
        {
            this.codItem = codItem;
            this.empresa = empresa;
            this.qtdLiberada = qtdLiberada;
            this.qtdReservada = qtdReservada;
            this.qtdPed = qtdPed;
            this.qtdOc = qtdOc;
            this.dataUltSP = dataUltSP;
            this.desc = desc;
        }

        public string CodItem { get => codItem; set => codItem = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public int QtdLiberada { get => qtdLiberada; set => qtdLiberada = value; }
        public int QtdReservada { get => qtdReservada; set => qtdReservada = value; }
        public int QtdPed { get => qtdPed; set => qtdPed = value; }
        public int QtdOc { get => qtdOc; set => qtdOc = value; }
        public DateTime DataUltSP { get => dataUltSP; set => dataUltSP = value; }
        public string Desc { get => desc; set => desc = value; }
    }
}
