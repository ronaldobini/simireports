using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Comissao
    {
        private int notaFiscal;
        private int numPed;
        private string item;
        private string nomCliente;
        private string desItem;
        private string qtdItem;
        private string precoUnitBruto;
        private double preTotal;
        private string pctComissao;
        private double comiss;
        private string nomRepres;
        private DateTime datAltSit;
        private DateTime datHorEmiss;
        
        public Comissao()
        {

        }

        public Comissao(int notaFiscal, int numPed, string item, string nomCliente, string desItem, string qtdItem, string precoUnitBruto, double preTotal, string pctComissao, double comiss, string nomRepres, DateTime datAltSit, DateTime datHorEmiss)
        {
            this.notaFiscal = notaFiscal;
            this.numPed = numPed;
            this.item = item;
            this.nomCliente = nomCliente;
            this.desItem = desItem;
            this.qtdItem = qtdItem;
            this.precoUnitBruto = precoUnitBruto;
            this.preTotal = preTotal;
            this.pctComissao = pctComissao;
            this.comiss = comiss;
            this.nomRepres = nomRepres;
            this.datAltSit = datAltSit;
            this.datHorEmiss = datHorEmiss;
        }

        public int NotaFiscal { get => notaFiscal; set => notaFiscal = value; }
        public int NumPed { get => numPed; set => numPed = value; }
        public string Item { get => item; set => item = value; }
        public string NomCliente { get => nomCliente; set => nomCliente = value; }
        public string DesItem { get => desItem; set => desItem = value; }
        public string QtdItem { get => qtdItem; set => qtdItem = value; }
        public string PrecoUnitBruto { get => precoUnitBruto; set => precoUnitBruto = value; }
        public double PreTotal { get => preTotal; set => preTotal = value; }
        public string PctComissao { get => pctComissao; set => pctComissao = value; }
        public double Comiss { get => comiss; set => comiss = value; }
        public string NomRepres { get => nomRepres; set => nomRepres = value; }
        public DateTime DatAltSit { get => datAltSit; set => datAltSit = value; }
        public DateTime DatHorEmiss { get => datHorEmiss; set => datHorEmiss = value; }
    }
}