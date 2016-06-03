using pjtCidadedeira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pjtCidadedeira.Controllers
{
    public class RelatoriosController : Controller
    {
        private CidadeiraDBContext db = new CidadeiraDBContext();
        // GET: Relatórios
        public ActionResult ReclamacaoPorPeriodo(String dataBuscar)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dataBuscar);

                if (dataBuscar != null)
                {
                    IQueryable<PorReclamacao> reclama = from reclamacao in db.Reclamacoes group reclamacao by reclamacao.Categoria into dateGroup select new PorReclamacao()
                    {
                        categoria = dateGroup.Key,
                        ReclamacaoCount = dateGroup.Count(s => s.Data >= dt)
                    };
                    return View(reclama.ToList());
                }
          
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Data Inválida');</script>");
               
            }
            return View();
        }
    }
}