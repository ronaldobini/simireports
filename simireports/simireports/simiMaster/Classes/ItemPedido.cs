using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simiMaster.Classes
{
    public class ItemPedido
    {
        private Item item;
        private int qtd;
        private int sequencia;

        public ItemPedido(Item item, int qtd, int sequencia)
        {
            this.item = item;
            this.qtd = qtd;
            this.sequencia = sequencia;
        }

        public Item Item { get => item; set => item = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public int Sequencia { get => sequencia; set => sequencia = value; }
    }
}