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
        public ActionResult ReclamacaoPorPeriodo(string opcao,String dataBuscar)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dataBuscar);
                
                if (dataBuscar != null && opcao !=null)
                {
                    if (opcao.Equals("categoria"))
                    {
                         IQueryable<PorReclamacao> reclama = from reclamacao in db.Reclamacoes group reclamacao by reclamacao.Categoria.Titulo into dateGroup select new PorReclamacao()
                        {
                            propriedade = dateGroup.Key,
                            ReclamacaoCount = dateGroup.Count(s => s.Data >= dt)
                        };
                        return View(reclama.ToList());
                    }
                    else
                    {
                         IQueryable<PorReclamacao> reclama = from reclamacao in db.Reclamacoes group reclamacao by reclamacao.Endereco.Bairro into dateGroup select new PorReclamacao()
                         {
                             propriedade = dateGroup.Key,
                            ReclamacaoCount = dateGroup.Count(s => s.Data >= dt)
                         };
                         return View(reclama.ToList());
                    }

                }else
                {
                     IQueryable<PorReclamacao> reclama = from reclamacao in db.Reclamacoes group reclamacao by reclamacao.Categoria.Titulo into dateGroup select new PorReclamacao()
                     {
                        propriedade = dateGroup.Key,
                        ReclamacaoCount = dateGroup.Count()
                     };
                     return View(reclama.ToList());
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Data Inválida');</script>");
               
            }
            return View();
        }

        // ComentarioPorCategoria
        public ActionResult ComentarioPorPeriodo(string opcao)
        {
            try
            {
               
                 if (opcao !=null)
                {
                    if (opcao.Equals("categoria"))
                    {
                          IQueryable<PorComentario> comenta = from comentario in db.Comentarios group comentario by comentario.Reclamacao.Categoria.Titulo into dateGroup select new PorComentario()
                            {
                                propriedade = dateGroup.Key,
                                ComentarioCount = dateGroup.Count()
                            };
                    
                            return View(comenta.ToList());
                    }
                    else if (opcao.Equals("bairro"))
                    {
                        IQueryable<PorComentario> comenta = from comentario in db.Comentarios group comentario by comentario.Reclamacao.Endereco.Bairro into dateGroup select new PorComentario()
                        {
                            propriedade = dateGroup.Key,
                            ComentarioCount = dateGroup.Count()
                        };
                    
                        return View(comenta.ToList());
                    }
                    else 
                    {
                        IQueryable<PorComentario> comenta = from comentario in db.Comentarios group comentario by comentario.Reclamacao.Data into dateGroup select new PorComentario()
                        {
                            propriedade = ""+dateGroup.Key,
                            ComentarioCount = dateGroup.Count()
                        };
                    
                        return View(comenta.ToList());
                    }

                }
                else
                { 
                    IQueryable<PorComentario> comenta = from comentario in db.Comentarios group comentario by comentario.Reclamacao.Endereco.Bairro into dateGroup select new PorComentario()
                    {
                        propriedade = dateGroup.Key,
                        ComentarioCount = dateGroup.Count()
                    };
                    
                    return View(comenta.ToList());
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Erro, Tenta mais tarde');</script>");

            }
            return View();
        }

        // PorcentagemPorStatus
        public ActionResult PorcentagemPorStatus(string opcao, string search)
        {
            try
            {
               
                var total = from rec in db.Reclamacoes select rec;
                if (!String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(opcao))
                {
                    if (opcao.Equals("categoria"))
                    {
                        total = total.Where(t => t.Categoria.Titulo.Contains(search));
                        int qtde = total.ToList().Count();
                        IQueryable<PorStatus> status = from reclamacao in db.Reclamacoes
                                                       where (reclamacao.Categoria.Titulo.Contains(search))
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
                        total = total.Where(t => t.Endereco.Bairro.Contains(search));
                        int qtde = total.ToList().Count();
                        IQueryable<PorStatus> status = from reclamacao in db.Reclamacoes
                                                       where (reclamacao.Endereco.Bairro.Contains(search))
                                                       group reclamacao by reclamacao.Status into dateGroup
                                                       select new PorStatus()
                                                       {
                                                           Status = dateGroup.Key,
                                                           StatusCount = (dateGroup.Count() * 100) / qtde
                                                       };

                        return View(status.ToList());
                    }
                    
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