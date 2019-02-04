using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Item
    {
        private string qtdSolic;
        private string qtdCancel;
        private string qtdAtend;
        private string nomeItem;
        private string przEntrega;

        public Item(string qtdSolic, string qtdCancel, string qtdAtend, string nomeItem, string przEntrega)
        {
            this.qtdSolic = qtdSolic;
            this.qtdCancel = qtdCancel;
            this.qtdAtend = qtdAtend;
            this.nomeItem = nomeItem;
            this.przEntrega = przEntrega;
        }

        public string QtdSolic { get => qtdSolic; set => qtdSolic = value; }
        public string QtdCancel { get => qtdCancel; set => qtdCancel = value; }
        public string QtdAtend { get => qtdAtend; set => qtdAtend = value; }
        public string NomeItem { get => nomeItem; set => nomeItem = value; }
        public string PrzEntrega { get => przEntrega; set => przEntrega = value; }
    }
}