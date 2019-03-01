using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simireports.simireports.Classes
{
    public class Metodos
    {
        public Metodos()
        {
        }
        public String formatarDecimal(Decimal n)
        {
            //nS - numero string
            //nF - numero formatado

            string nS = n.ToString();
            nS = pontoPorVirgula(nS);
            string nF = "";
            int inicio;
            if (nS.Contains(","))
            {
                for (int i = nS.Length - 1, j = 1; j <= 3; --i, ++j)
                {
                    if (nS.Length - i < 0)
                    {
                        break;
                    }
                    nF += nS[i];
                    if (i == nS.IndexOf(","))
                    {
                        break;
                    }
                }
                inicio = nS.IndexOf(",") - 1;
            }
            else
            {
                inicio = nS.Length - 1;
            }
            for (int i = inicio, j = 0; i >= 0; --i, ++j)
            {
                nF += nS[i];
                if (j == 2 && i > 0)
                {
                    j = -1;
                    nF += ".";
                }
            }

            nF = new string(nF.Reverse().ToArray());
            return nF;
        }
        public String pontoPorVirgula(String s)
        {
            if (s.Contains("."))
                s = s.Replace(".", ",");
            return s;
        }
        public String virgulaPorPonto(String s)
        {
            if (s.Contains(","))
                s = s.Replace(",", ".");
            return s;
        }

        public String configCoringas(String s)
        {
            if (s.Contains("*") || s.Contains("?"))
            {
                s = s.Replace("*", "%");
                s = s.Replace("?", "%");
            }
            return s;
        }
    }
}