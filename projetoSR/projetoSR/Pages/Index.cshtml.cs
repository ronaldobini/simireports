using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetoSR.Pages
{
    public class IndexModel : PageModel
    {

        
        public String resultExe;

        public void OnGet()
        {

            DAO.BancoAzure ba = new DAO.BancoAzure();
            String sql = "";
            //resultExe = ba.executar(sql);






        }
    }
}