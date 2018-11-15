using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidoVirtualWeb.Models;

namespace PedidoVirtualWeb.Controllers
{
    public class ComentariosController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Item).Include(c => c.Usuario);
            return View(comentarios.ToList());
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
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComentarioId,ItemId,UsuarioId,Mensagem,Data")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", comentario.ItemId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", comentario.UsuarioId);
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
            //ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", comentario.ItemId);
            //ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", comentario.UsuarioId);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComentarioId,ItemId,UsuarioId,Mensagem,Data")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                if (!comentario.Mensagem.Trim().Equals(""))
                {
                    comentario.Data = DateTime.Now;
                    db.Entry(comentario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { id = comentario.ItemId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "O comentário não pode ficar vazio!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "O comentário não pode ficar vazio!");
            }
            //ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", comentario.ItemId);
            //ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "Nome", comentario.UsuarioId);
            comentario = db.Comentarios.Find(comentario.ItemId);
            return View(comentario);
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
            var itemId = comentario.ItemId;
            db.Comentarios.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Details", "Home", new { id = itemId });
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
