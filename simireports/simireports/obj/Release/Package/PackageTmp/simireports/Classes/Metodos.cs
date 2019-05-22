using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
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
            if ((nF.Length - (nF.IndexOf(",") + 1)) == 1)
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



        public static string pegarIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }




        public static string inserirLog(int idUser, string acao, string obs1, string obs2)
        {
            //string ip = pegarIP();
            //string pc = Dns.GetHostName();

            string ip = "-";
            string pc = "-";

            string tempo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            SqlConnection conn = new BancoAzure().abrir();
            string result = "-";
            obs2 = pc + "|" + obs2;
            if (idUser != 58)
            {
                string sql = "INSERT INTO sw_log (ip,id_user,tempo,acao,obs1,obs2) VALUES ('" + ip + "', " + idUser + ", '" + tempo + "', '" + acao + "', '" + obs1 + "', '" + obs2 + "')";
                result = new BancoAzure().executar(sql, conn);
            }
            new BancoAzure().fechar(conn);
            return result;
        }



        public string senhaNv1p0(string aa)
        {
            string resultado = "";
            //Calculo: raiz de (n*15), e pega os 6 numeros depois da virgula
            if (aa != "")
            {
                double numero = Convert.ToDouble(aa);
                double calculo = Math.Sqrt((numero * 15));
                aa = Convert.ToString(calculo);
                int virg = aa.IndexOf(",") + 1;
                for (int i = virg; i < virg + 6 && i < aa.Length; ++i)
                {
                    resultado += aa[i];
                }
            }
            return resultado;
        }


        public string senhaNv1p5(string aa)
        {
            string resultado = "";
            //Calculo: raiz de (n), e pega os 6 numeros depois da virgula
            if (aa != "")
            {
                double numero = Convert.ToDouble(aa);
                double calculo = Math.Sqrt((numero));
                aa = Convert.ToString(calculo);
                int virg = aa.IndexOf(",") + 1;
                for (int i = virg; i < virg + 6 && i < aa.Length; ++i)
                {
                    resultado += aa[i];
                }
            }
            return resultado;
        }


        public string senhaNv2p0(string aa)
        {
            string resultado = "";
            //Valor do dia da semana: 
            //Sabado e Domingo - 0
            //Segunda - 1
            //Terca - 2
            //Quarta - 3
            //Quinta - 4
            //Sexta - 5
            //Calculo: Adiciona em todos os digitos o valor do digito na posicao <valor do dia da semana>
            if (aa != "")
            {
                DayOfWeek dow = DateTime.Now.DayOfWeek;
                int posSoma;
                if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
                {
                    posSoma = 0;
                }
                else if (dow == DayOfWeek.Monday)
                {
                    posSoma = 1;
                }
                else if (dow == DayOfWeek.Tuesday)
                {
                    posSoma = 2;
                }
                else if (dow == DayOfWeek.Wednesday)
                {
                    posSoma = 3;
                }
                else if (dow == DayOfWeek.Thursday)
                {
                    posSoma = 4;
                }
                else
                {
                    posSoma = 5;
                }
                int somador = Convert.ToInt32(aa[posSoma].ToString());
                for (int i = 0; i < 6; ++i)
                {
                    int valor = int.Parse(aa[i].ToString()) + somador;
                    if (valor >= 10)
                    {
                        valor = 0;
                    }
                    resultado += valor;
                }
            }
            return resultado;
        }



        public static int numAleatorio(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string stringAleatoria(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string senhaAleatoria()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(stringAleatoria(1, false));
            builder.Append(stringAleatoria(5, true));
            builder.Append(numAleatorio(1000, 9999));
            return builder.ToString();
        }


        public static void linkarTabelasUser(int idUser, int nivel)
        {
            try
            {
                SqlConnection conn = new BancoAzure().abrir();
                SqlConnection conn2 = new BancoAzure().abrir();
                string sql = "SELECT id,senha_rand from sw_usuarios where id_crm = " + idUser;

                SqlDataReader reader = new BancoAzure().consultar(sql, conn);
                string tempo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                reader.Read();
                if (reader.HasRows)
                {
                    int idsw = reader.GetInt32(0);
                    string randSenha = "";
                    if (reader.IsDBNull(1))
                    {
                        randSenha = senhaAleatoria();
                    }
                    else
                    {
                        randSenha = reader.GetString(1);
                    }
                    new BancoAzure().executar("UPDATE sw_usuarios SET ult_login = '" + tempo + "', nivel = " + nivel + ", senha_rand = '" + randSenha + "' where id = " + idsw, conn2);
                }
                else
                {
                    string vazio = "-";
                    string randSenha = senhaAleatoria();
                    string result = new BancoAzure().executar("INSERT INTO sw_usuarios (id_crm, funcao, grupo, nivel, ult_login, erros_senha, gerenciados, senha_rand, avisos, block) " +
                                                                "VALUES (" + idUser + ",'" + vazio + "','" + vazio + "'," + nivel + ",'" + tempo + "', 0" +
                                                                ",'" + vazio + "','" + randSenha + "','" + vazio + "',0)", conn2);
                }
            }
            catch (Exception ex)
            {
                string erro = "erro linkar tabelas users: " + ex;
            }

        }

        public int qtdLogixToInt(string qtdS)
        {
            return Convert.ToInt32(Decimal.Parse(pontoPorVirgula(qtdS)));
        }
    }
}