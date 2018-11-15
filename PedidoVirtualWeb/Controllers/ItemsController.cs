using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidoVirtualWeb.Classes;
using PedidoVirtualWeb.Models;

namespace PedidoVirtualWeb.Controllers
{
    public class ItemsController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Categoria);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nome");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            try
            {
                db.Items.Add(item);
                db.SaveChanges();

                if (item.FotoFile != null)
                {
                    var folder = "/Content/ItemFoto";
                    var file = string.Format("{0}.jpg", item.ItemId);

                    var atualizadoFoto = FilesHelper.UploadPhoto(item.FotoFile, folder, file);
                    if (atualizadoFoto)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        item.Foto = pic;
                    }
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (db.Items.Where(c => c.Nome.Equals(item.Nome)).Any())
                    ModelState.AddModelError(string.Empty, "Já existe um item com este nome.");
                else
                    ModelState.AddModelError(string.Empty, ex.Message);
            }


            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (item.FotoFile != null)
                    {
                        var folder = "/Content/ItemFoto";
                        var file = string.Format("{0}.jpg", item.ItemId);

                        var atualizadoFoto = FilesHelper.UploadPhoto(item.FotoFile, folder, file);
                        if (atualizadoFoto)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            item.Foto = pic;
                        }
                    }

                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (db.Items.Where(c => c.Nome.Equals(item.Nome)).Any())
                        ModelState.AddModelError(string.Empty, "Já existe um item com este nome.");
                    else
                        ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(), "CategoriaId", "Nome", item.CategoriaId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
