using System;
using System.Collections.Generic;

namespace simireports.simireports.Classes
{
    public class Item
    {
        private string codItem;
        private int qtdSolic;
        private int qtdCancel;
        private int qtdAtend;
        private string nomeItem;
        private Decimal precoUnit;
        private string przEntrega;
        private Decimal desconto;
        private int pedLogix;
        private Decimal comiss;
        private int qtdRom;
        private int qtdLib;
        private int qtdRes;
        private List<OrdemCompra> OCs;

        public Item(string codItem, int qtdSolic, int qtdCancel, int qtdAtend,
            string nomeItem, decimal precoUnit, string przEntrega, decimal desconto,
            int pedLogix, decimal comiss, int qtdRom, int qtdLib, int qtdRes)
        {
            this.codItem = codItem;
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.precoUnit = precoUnit;
            this.przEntrega = przEntrega;
            this.desconto = desconto;
            this.pedLogix = pedLogix;
            this.comiss = comiss;
            this.qtdRom = qtdRom;
            this.qtdLib = qtdLib;
            this.qtdRes = qtdRes;
        }

        public string NomeItem { get => nomeItem; set => nomeItem = value; }
        public string PrzEntrega { get => przEntrega; set => przEntrega = value; }
        public string CodItem { get => codItem; set => codItem = value; }
        public decimal PrecoUnit { get => precoUnit; set => precoUnit = value; }
        public decimal Desconto { get => desconto; set => desconto = value; }
        public int PedLogix { get => pedLogix; set => pedLogix = value; }
        public decimal Comiss { get => comiss; set => comiss = value; }
        public int QtdRom { get => qtdRom; set => qtdRom = value; }
        public int QtdLib { get => qtdLib; set => qtdLib = value; }
        public int QtdRes { get => qtdRes; set => qtdRes = value; }
        public int QtdSolic { get => qtdSolic; set => qtdSolic = value; }
        public int QtdCancel { get => qtdCancel; set => qtdCancel = value; }
        public int QtdAtend { get => qtdAtend; set => qtdAtend = value; }
        public List<OrdemCompra> OCs1 { get => OCs; set => OCs = value; }
    }
}