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
    public class PedidoItemsController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();

        // GET: PedidoItems
        public ActionResult Index()
        {
            var pedidoItems = db.PedidoItems.Include(p => p.Item).Include(p => p.Pedido);
            return View(pedidoItems.ToList());
        }

        // GET: PedidoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoItem pedidoItem = db.PedidoItems.Find(id);
            if (pedidoItem == null)
            {
                return HttpNotFound();
            }
            return View(pedidoItem);
        }

        // GET: PedidoItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto");
            ViewBag.PedidoId = new SelectList(db.Pedidos, "PedidoId", "PedidoId");
            return View();
        }

        // POST: PedidoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,ItemId,Quantidade")] PedidoItem pedidoItem)
        {
            if (ModelState.IsValid)
            {
                db.PedidoItems.Add(pedidoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", pedidoItem.ItemId);
            ViewBag.PedidoId = new SelectList(db.Pedidos, "PedidoId", "PedidoId", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // GET: PedidoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoItem pedidoItem = db.PedidoItems.Find(id);
            if (pedidoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", pedidoItem.ItemId);
            ViewBag.PedidoId = new SelectList(db.Pedidos, "PedidoId", "PedidoId", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // POST: PedidoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,ItemId,Quantidade")] PedidoItem pedidoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Foto", pedidoItem.ItemId);
            ViewBag.PedidoId = new SelectList(db.Pedidos, "PedidoId", "PedidoId", pedidoItem.PedidoId);
            return View(pedidoItem);
        }

        // GET: PedidoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoItem pedidoItem = db.PedidoItems.Find(id);
            if (pedidoItem == null)
            {
                return HttpNotFound();
            }
            return View(pedidoItem);
        }

        // POST: PedidoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoItem pedidoItem = db.PedidoItems.Find(id);
            db.PedidoItems.Remove(pedidoItem);
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
