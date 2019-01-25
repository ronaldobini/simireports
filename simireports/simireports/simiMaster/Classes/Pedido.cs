using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simiMaster.Classes{
    
    public class Pedido{

        private int codPedido;
        private int empresa;
        private List<ItemPedido> itens;

        public Pedido(){

        }

        public Pedido(int codPedido, int empresa, List<ItemPedido> itens){
            this.codPedido = codPedido;
            this.itens = itens;
            this.empresa = empresa;
        }

        public int CodPedido { get => codPedido; set => codPedido = value; }
        public List<ItemPedido> Itens { get => itens; set => itens = value; }
        public int Empresa { get => empresa; set => empresa = value; }
    }
}