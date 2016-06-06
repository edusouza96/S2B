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
        // ReclamaçãoPorPeriodo
        public ActionResult ReclamacaoPorPeriodo(String dataBuscar)// refaturar depois para colocar por bairro
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

        // ComentarioPorCategoria
        public ActionResult ComentarioPorPeriodo()
        {
            try
            {
                IQueryable<PorComentario> comenta = from comentario in db.Comentarios group comentario by comentario.Reclamacao.Categoria into dateGroup select new PorComentario()
                    {
                        categoria = dateGroup.Key,
                        ComentarioCount = dateGroup.Count()
                    };
                    
                    return View(comenta.ToList());
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Erro, Tenta mais tarde');</script>");

            }
            return View();
        }

        // PorcentagemPorStatus
        public ActionResult PorcentagemPorStatus(string categoriaSearch)
        {
            try
            {
               
                var total = from rec in db.Reclamacoes select rec;
                if (!String.IsNullOrEmpty(categoriaSearch))
                {
                    total = total.Where(t => t.Categoria.Titulo.Contains(categoriaSearch));
                    int qtde = total.ToList().Count();
                    IQueryable<PorStatus> status = from reclamacao in db.Reclamacoes where (reclamacao.Categoria.Titulo.Contains(categoriaSearch))
                                                   group reclamacao by reclamacao.Status into dateGroup
                                                   select new PorStatus()
                                                   {
                                                       Status = dateGroup.Key,
                                                       StatusCount = (dateGroup.Count() * 100) / qtde
                                                   };

                    return View(status.ToList());
                }
                else
                {
                    int qtde = total.ToList().Count();
                    IQueryable<PorStatus> entrada = from reclamacao in db.Reclamacoes
                                                    group reclamacao by reclamacao.Status into dateGroup
                                                    select new PorStatus()
                                                    {
                                                        Status = dateGroup.Key,
                                                        StatusCount = (dateGroup.Count() * 100) / qtde
                                                    };
                    return View(entrada.ToList());
                }
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Erro, Tenta mais tarde');</script>");

            }
            return View();
        }

        
    }
}