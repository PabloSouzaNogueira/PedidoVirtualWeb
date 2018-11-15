using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidoVirtualWeb.App_Start;
using PedidoVirtualWeb.Classes;
using PedidoVirtualWeb.Models;

namespace PedidoVirtualWeb.Controllers
{
    public class PedidosController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidoes = db.Pedidos.Include(p => p.Status).Include(p => p.Usuario);
            return View(pedidoes.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.StatusPedidoId = new SelectList(CombosHelper.GetStatus(), "StatusPedidoId", "Nome");
            ViewBag.UsuarioId = new SelectList(CombosHelper.GetUsuarios(), "UsuarioId", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,UsuarioId,StatusPedidoId,Data")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusPedidoId = new SelectList(CombosHelper.GetStatus(), "StatusPedidoId", "Nome", pedido.StatusPedidoId);
            ViewBag.UsuarioId = new SelectList(CombosHelper.GetUsuarios(), "UsuarioId", "Nome", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusPedidoId = new SelectList(CombosHelper.GetStatus(), "StatusPedidoId", "Nome", pedido.StatusPedidoId);
            ViewBag.UsuarioId = new SelectList(CombosHelper.GetUsuarios(), "UsuarioId", "Nome", pedido.UsuarioId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,UsuarioId,StatusPedidoId,Data")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusPedidoId = new SelectList(CombosHelper.GetStatus(), "StatusPedidoId", "Nome", pedido.StatusPedidoId);
            ViewBag.UsuarioId = new SelectList(CombosHelper.GetUsuarios(), "UsuarioId", "Nome", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MeusPedidos(string email)
        {
            var usuario = db.Usuarios.Where(U => U.UserName.Equals(email)).FirstOrDefault();

            //var itensTemp = PedidoBase.Carrinho.Select(C => C.Item).ToList();
            //var itens = db.Items.Where(I => itensTemp.Contains(I)).ToList();

            ViewBag.Carrinho = PedidoBase.Carrinho;
            ViewBag.CarrinhoVazio = PedidoBase.Carrinho.Count() == 0 ? true : false;

            var pedidos = new List<Pedido>();

            if (usuario != null)
            {
                pedidos = db.Pedidos.Where(P => P.UsuarioId == usuario.UsuarioId).ToList();
            }

            return View(pedidos);
        }

        public ActionResult VisualizarPedido(int pedidoId)
        {
            var pedido = db.Pedidos.Where(P => P.PedidoId == pedidoId).FirstOrDefault();
            var pedidoItem = db.PedidoItems.Where(PI => PI.PedidoId == pedidoId).ToList();
            double total = 0;

            foreach (PedidoItem PI in pedidoItem)
            {
                total += PI.Quantidade * PI.Item.Preco;
            }

            ViewBag.Total = total;

            return View(pedido);
        }

        public ActionResult ConfirmarCompra()
        {
            if (!PedidoBase.Usuario.Equals(""))
            {
                if (PedidoBase.Carrinho.Count() != 0)
                {
                    Usuario usuario = db.Usuarios.Where(U => U.UserName.Equals(PedidoBase.Usuario)).FirstOrDefault();

                    Pedido P = new Pedido();
                    P.StatusPedidoId = 1;
                    P.UsuarioId = usuario.UsuarioId;
                    P.Data = DateTime.Now;
                    db.Pedidos.Add(P);
                    db.SaveChanges();

                    int pedidoID = db.Pedidos.Select(pedido => pedido.PedidoId).OrderByDescending(pedido => pedido).FirstOrDefault();

                    foreach (PedidoItem PI in PedidoBase.Carrinho)
                    {
                        PedidoItem pedidoItem = new PedidoItem();
                        pedidoItem.PedidoId = pedidoID;
                        pedidoItem.ItemId = PI.ItemId;
                        pedidoItem.Quantidade = PI.Quantidade;
                        db.PedidoItems.Add(pedidoItem);
                    }

                    db.SaveChanges();

                    PedidoBase.Carrinho = new List<PedidoItem>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Carrinho está vazio.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Favor logar no sistema.");
            }

            return RedirectToAction("MeusPedidos", new { email = User.Identity.Name });
        }

        public ActionResult RemoverPedidoItem(int itemId)
        {
            var item = db.Items.Where(I => I.ItemId == itemId).FirstOrDefault();
            var itemCarrinho = PedidoBase.Carrinho.Where(C => C.Item.ItemId == itemId).FirstOrDefault();

            item.Estoque = item.Estoque + itemCarrinho.Quantidade;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            PedidoBase.Carrinho.RemoveAll(C => C.Item.ItemId == itemId);

            return RedirectToAction("MeusPedidos", new { email = User.Identity.Name });
        }

        public ActionResult EditarPedidoItem(int itemId)
        {
            var item = db.Items.Where(I => I.ItemId == itemId).FirstOrDefault();
            var pedidoItem = PedidoBase.Carrinho.Where(C => C.Item.ItemId == itemId).FirstOrDefault();

            item.Estoque = item.Estoque + pedidoItem.Quantidade;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            return View(pedidoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPedidoItem([Bind(Include = "PedidoId,ItemId,Quantidade")] PedidoItem pedidoItem)
        {
            var item = db.Items.Where(I => I.ItemId == pedidoItem.ItemId).FirstOrDefault();

            if (item.Estoque > pedidoItem.Quantidade)
            {
                PedidoBase.Carrinho.Where(C => C.Item.ItemId == pedidoItem.ItemId).FirstOrDefault().Quantidade = pedidoItem.Quantidade;
                item.Estoque = item.Estoque - pedidoItem.Quantidade;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Não existe estoque suficiente do item.");
                return View(pedidoItem);
            }

            return RedirectToAction("MeusPedidos", new { email = User.Identity.Name });
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
