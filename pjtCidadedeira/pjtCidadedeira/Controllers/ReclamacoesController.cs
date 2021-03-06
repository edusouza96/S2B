﻿using System;
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
    public class ReclamacoesController : Controller
    {
        private CidadeiraDBContext db = new CidadeiraDBContext();

        // GET: Reclamacoes
        public ViewResult Index(string search, int? selectedCategoria, int ? selectedEndereco, int ? selectedUsuarios)
        {
            var categorias = db.Categorias.OrderBy(g => g.Titulo).ToList();
            ViewBag.selectedCategoria = new SelectList(categorias, "CategoriaID", "Titulo", selectedCategoria);
            int categoriaID = selectedCategoria.GetValueOrDefault();

            var enderecos = db.Enderecos.OrderBy(g => g.Logradouro).ToList();
            ViewBag.selectedEndereco = new SelectList(enderecos, "EnderecoID", "Logradouro", selectedEndereco);
            int enderecoID = selectedEndereco.GetValueOrDefault();

            var usuarios = db.Usuarios.OrderBy(g => g.User).ToList();
            ViewBag.selectedUsuarios = new SelectList(usuarios, "UsuarioID", "User", selectedUsuarios);
            int usuarioID = selectedUsuarios.GetValueOrDefault();

            var reclamacao = db.Reclamacoes.Where(c => !selectedCategoria.HasValue || c.CategoriaID == categoriaID);
            reclamacao = reclamacao.Where(c1 => !selectedEndereco.HasValue || c1.EnderecoID == enderecoID);

            if (!String.IsNullOrEmpty(search))
            {
                reclamacao = reclamacao.Where(s => s.Titulo.Contains(search));
            }
            return View(reclamacao.ToList());
        }

        // GET: Reclamacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        // GET: Reclamacoes/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo");
            ViewBag.EnderecoID = new SelectList(db.Enderecos, "EnderecoID", "Logradouro");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "User");

            return View();
        }

        // POST: Reclamacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReclamacaoID,Titulo,Descricao,CategoriaID,Data,LinkImagem,Status,UsuarioID,EnderecoID")] Reclamacao reclamacao)
        {
            if (ModelState.IsValid)
            {
                reclamacao.Data = DateTime.Now;
                reclamacao.Status = "Aberto";
                db.Reclamacoes.Add(reclamacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", reclamacao.CategoriaID);
            ViewBag.EnderecoID = new SelectList(db.Enderecos, "EnderecoID", "Logradouro", reclamacao.EnderecoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "User", reclamacao.UsuarioID);

            return View(reclamacao);
        }

        // GET: Reclamacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", reclamacao.CategoriaID);
            ViewBag.EnderecoID = new SelectList(db.Enderecos, "EnderecoID", "Logradouro", reclamacao.EnderecoID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "User", reclamacao.UsuarioID);

            return View(reclamacao);
        }

        // POST: Reclamacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReclamacaoID,Titulo,Descricao,CategoriaID,Data,LinkImagem,Status,UsuarioID,EnderecoID")] Reclamacao reclamacao)
        {
            if (ModelState.IsValid)
            {
                reclamacao.Data = DateTime.Now;
                db.Entry(reclamacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reclamacao);
        }

        // GET: Reclamacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        // POST: Reclamacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            db.Reclamacoes.Remove(reclamacao);
            db.SaveChanges();
            return RedirectToAction("Index");
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
