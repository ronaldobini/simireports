using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string QtdSolic { get => qtdSolic; set => qtdSolic = value; }
        public string QtdCancel { get => qtdCancel; set => qtdCancel = value; }
        public string QtdAtend { get => qtdAtend; set => qtdAtend = value; }
        public string NomeItem { get => nomeItem; set => nomeItem = value; }
        public string PrzEntrega { get => przEntrega; set => przEntrega = value; }
        public string CodItem { get => codItem; set => codItem = value; }
        public decimal PrecoUnit { get => precoUnit; set => precoUnit = value; }
        public decimal Desconto { get => desconto; set => desconto = value; }
    }
}