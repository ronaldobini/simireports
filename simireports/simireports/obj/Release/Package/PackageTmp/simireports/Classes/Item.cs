using System;

namespace simireports.simireports.Classes
{
    public class Item
    {
        private string codItem;
        private string qtdSolic;
        private string qtdCancel;
        private string qtdAtend;
        private string nomeItem;
        private Decimal precoUnit;
        private string przEntrega;
        private Decimal desconto;
        private int pedLogix;
        private Decimal comiss;
        private string qtdRom;
        private string qtdLib;
        private string qtdRes;


        public Item(string qtdSolic, string descItem, string codItem, Decimal preUnit)
        {
            this.qtdAtend = qtdSolic;
            this.nomeItem = descItem;
            this.codItem = codItem;
            this.precoUnit = preUnit;
        }

        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
        }
        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem, Decimal precoUnit)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
        }
        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem, Decimal precoUnit, string qtdRom, string qtdLib, string qtdRes)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
            this.qtdRom = qtdRom;
            this.qtdLib = qtdLib;
            this.qtdRes = qtdRes;
        }

        
        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem, Decimal precoUnit, Decimal desconto)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
            this.desconto = desconto;
        }
        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem, Decimal precoUnit, Decimal desconto,int pedLogix)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
            this.desconto = desconto;
            this.pedLogix = pedLogix;
        }
        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega, string codItem, Decimal precoUnit, Decimal desconto, Decimal comiss)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
            this.desconto = desconto;
            this.pedLogix = pedLogix;
            this.comiss = comiss;
        }


       



        public string QtdSolic { get => qtdSolic; set => qtdSolic = value; }
        public string QtdCancel { get => qtdCancel; set => qtdCancel = value; }
        public string QtdAtend { get => qtdAtend; set => qtdAtend = value; }
        public string NomeItem { get => nomeItem; set => nomeItem = value; }
        public string PrzEntrega { get => przEntrega; set => przEntrega = value; }
        public string CodItem { get => codItem; set => codItem = value; }
        public decimal PrecoUnit { get => precoUnit; set => precoUnit = value; }
        public decimal Desconto { get => desconto; set => desconto = value; }
        public int PedLogix { get => pedLogix; set => pedLogix = value; }
        public decimal Comiss { get => comiss; set => comiss = value; }
        public string QtdRom { get => qtdRom; set => qtdRom = value; }
        public string QtdLib { get => qtdLib; set => qtdLib = value; }
        public string QtdRes { get => qtdRes; set => qtdRes = value; }
    }
}