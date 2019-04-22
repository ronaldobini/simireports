using System;
using System.Collections.Generic;
using System.Text;

namespace FupApp.Classes
{
    class FollowUp
    {
        string proposta;
        string fup;
        DateTime data;
        string quem;
        string quemSup;

        public FollowUp(string proposta, string fup, DateTime data, string quem, string quemSup)
        {
            this.proposta = proposta;
            this.fup = fup;
            this.data = data;
            this.quem = quem;
            this.quemSup = quemSup;
        }

        public string Proposta { get => proposta; set => proposta = value; }
        public string Fup { get => fup; set => fup = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Quem { get => quem; set => quem = value; }
        public string QuemSup { get => quemSup; set => quemSup = value; }
    }
}
