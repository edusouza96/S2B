using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pjtCidadedeira.Models;

namespace pjtCidadedeira.Controllers
{
    public class ComentariosController : Controller
    {
        private CidadeiraDBContext db = new CidadeiraDBContext();

        // GET: Comentarios
        public ActionResult Index(int?id, int?selectedReclamacao, int? selectedUsuarios)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reclamacoes = db.Reclamacoes.OrderBy(g => g.Titulo).ToList();
            ViewBag.selectedReclamacao = new SelectList(reclamacoes, "ReclamacaoID", "Titulo", selectedReclamacao);
            int ReclamacaoID = selectedReclamacao.GetValueOrDefault();

            var usuarios = db.Usuarios.OrderBy(g => g.User).ToList();
            ViewBag.selectedUsuarios = new SelectList(usuarios, "UsuariosID", "User", selectedUsuarios);
            int UsuarioID = selectedUsuarios.GetValueOrDefault();

            var comentario = db.Comentarios.Where(c => !selectedReclamacao.HasValue || c.ReclamacaoID == ReclamacaoID);
            comentario = comentario.Where(c => c.ReclamacaoID == id);
            return View(comentario.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "User");

            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComentarioID,descricaoComentario,Data,UsuarioID,LinkImagem,ReclamacaoID")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.Data = DateTime.Now;
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("../Reclamacoes/Index");
            }

            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuariosID", "User", comentario.UsuarioID);

            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "User", comentario.UsuarioID);

            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComentarioID,descricaoComentario,Data,UsuarioID,LinkImagem,ReclamacaoID")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.Data = DateTime.Now;
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Reclamacoes/Index");
                // return RedirectToAction("Index");
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuariosID", "User", comentario.UsuarioID);

            return RedirectToAction("../Reclamacoes/Index");
            //  return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("../Reclamacoes/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
