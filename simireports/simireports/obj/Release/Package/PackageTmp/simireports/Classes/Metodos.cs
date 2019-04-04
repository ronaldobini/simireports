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
                nF += "00,";
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
            if ((nF.Length - (nF.IndexOf(",")+1)) == 1)
            {
                nF += "0";
            }
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
        public String configDataHuman2Banco(String data)
        {
            if (!data.Equals(""))
            {
                DateTime dt = Convert.ToDateTime(data);
                string dataConv = "";
                dataConv = dt.ToString("yyyy-MM-dd"/* HH:mm:ss"*/);//mudei pra nao por hora pra por direto la no select, pra dai poder colocar 23:59:59

                return dataConv;
            }
            else
            {
                return "";
            }
        }
        public String configDataBanco2Human(String data)
        {
            if (!data.Equals(""))
            {
                DateTime dt = Convert.ToDateTime(data);
                string dataConv = "";
                dataConv = dt.ToString("dd/MM/yyyy");

                return dataConv;
            }
            else
            {
                return "";
            }
        }
    }
}