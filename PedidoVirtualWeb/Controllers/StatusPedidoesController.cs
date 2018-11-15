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
    public class StatusPedidoesController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();

        // GET: StatusPedidoes
        public ActionResult Index()
        {
            return View(db.StatusPedidos.ToList());
        }

        // GET: StatusPedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidos.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // GET: StatusPedidoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusPedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusPedidoId,Nome")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.StatusPedidos.Add(statusPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusPedido);
        }

        // GET: StatusPedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidos.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusPedidoId,Nome")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusPedido);
        }

        // GET: StatusPedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidos.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusPedido statusPedido = db.StatusPedidos.Find(id);
            db.StatusPedidos.Remove(statusPedido);
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
