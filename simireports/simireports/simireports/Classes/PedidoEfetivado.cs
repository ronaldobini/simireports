using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class PedidoEfetivado
    {
        private string codEmpresa;
        private DateTime dat;
        private string codCliente;
        private string numPed;
        private string qtdPecasSolic;
        private string qtdPecasCancel;
        private string codItem;
        private Decimal precoUnit;
        private string cliente;
        private string repres;

        public PedidoEfetivado(string codEmpresa, DateTime dat, string codCliente, string numPed, string qtdPecasSolic, string qtdPecasCancel, string codItem, decimal precoUnit, string cliente,string repres)
        {
            this.codEmpresa = codEmpresa;
            this.dat = dat;
            this.codCliente = codCliente;
            this.numPed = numPed;
            this.qtdPecasSolic = qtdPecasSolic;
            this.qtdPecasCancel = qtdPecasCancel;
            this.codItem = codItem;
            this.precoUnit = precoUnit;
            this.cliente = cliente;
            this.repres = repres;
        }

        public string CodEmpresa { get => codEmpresa; set => codEmpresa = value; }
        public DateTime Dat { get => dat; set => dat = value; }
        public string CodCliente { get => codCliente; set => codCliente = value; }
        public string NumPed { get => numPed; set => numPed = value; }
        public string QtdPecasSolic { get => qtdPecasSolic; set => qtdPecasSolic = value; }
        public string QtdPecasCancel { get => qtdPecasCancel; set => qtdPecasCancel = value; }
        public string CodItem { get => codItem; set => codItem = value; }
        public decimal PrecoUnit { get => precoUnit; set => precoUnit = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Repres { get => repres; set => repres = value; }
    }
}