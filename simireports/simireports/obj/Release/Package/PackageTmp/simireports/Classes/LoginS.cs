using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class LoginS
    {
        private string nome;
        private string senha;
        private double nivel;

        public LoginS(string nome, string senha, double nivel)
        {
            this.nome = nome;
            this.senha = senha;
            this.nivel = nivel;
        }

        public string Nome { get => nome; set => nome = value; }
        public string Senha { get => senha; set => senha = value; }
        public double Nivel { get => nivel; set => nivel = value; }
    }
}