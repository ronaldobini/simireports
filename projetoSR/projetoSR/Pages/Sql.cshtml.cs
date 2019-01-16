using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetoSR.Pages
{
    public class SqlModel : PageModel
    {
        public string result = "-";

        public void OnGet()
        {

            string sql = "select nome from Usuarios";
            result = new DAO.BancoAzure().consultar(sql);
        }
    }
}