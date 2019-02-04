using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simiMaster.Classes
{
    public class Item
    {
        private string codItem;
        private Decimal preco;

        public Item(string codItem, Decimal preco)
        {
            this.codItem = codItem;
            this.preco = preco;
        }

        public string CodItem { get => codItem; set => codItem = value; }
        public decimal Preco { get => preco; set => preco = value; }
    }
}