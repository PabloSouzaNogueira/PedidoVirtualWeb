using PedidoVirtualWeb.App_Start;
using PedidoVirtualWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PedidoVirtualWeb.Controllers
{
    public class HomeController : Controller
    {
        private PedidoVirtualContext db = new PedidoVirtualContext();


        // GET: Main
        public ActionResult Index()
        {
            PedidoBase.Usuario = User.Identity.Name;

            ViewBag.Salgados = db.Items.Where(I => I.Categoria.Nome.Equals("Salgados"));
            ViewBag.Doces = db.Items.Where(I => I.Categoria.Nome.Equals("Doces"));
            ViewBag.Bebidas = db.Items.Where(I => I.Categoria.Nome.Equals("Bebidas"));
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);

            ViewBag.Adicionado = PedidoBase.Carrinho.Where(C => C.ItemId == id).Any(); //PedidoBase.Items.Where(I => I == id).Any();

            List<Comentario> comentarios = db.Comentarios.Where(C => C.Item.ItemId == id).ToList();
            ViewBag.Comentarios = comentarios;
            ViewBag.Usuario = PedidoBase.Usuario;
            ViewBag.Comentado = comentarios.Where(c => c.Usuario.UserName == PedidoBase.Usuario).Any();

            if (item.Estoque == 0)
            {
                ModelState.AddModelError(string.Empty, "Item esgotado!");
            }

            if (PedidoBase.Usuario.Equals(""))
            {
                ModelState.AddModelError(string.Empty, "É necessário estar logado para comentar.");
            }

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarComentario(Item item) //[Bind(Include = "ItemId,CategoriaId,Foto,Nome,Descricao,Preco,Estoque,ComentarioTemporario")] 
        {
            if (!PedidoBase.Usuario.Equals(""))
            {
                Usuario usuario = db.Usuarios.Where(U => U.UserName.Equals(PedidoBase.Usuario)).FirstOrDefault();

                if (!item.ComentarioTemporario.Trim().Equals(""))
                {
                    Comentario comentario = new Comentario();
                    comentario.Mensagem = item.ComentarioTemporario;
                    comentario.UsuarioId = usuario.UsuarioId;
                    comentario.ItemId = item.ItemId;
                    comentario.Data = DateTime.Now;

                    db.Comentarios.Add(comentario);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Details", new { id = item.ItemId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "ItemId,CategoriaId,Foto,Nome,Descricao,Preco,Estoque,ComentarioTemporario")] Item item)
        {
            if (item.Estoque > 0)
            {
                var itemTemp = db.Items.Where(I => I.ItemId == item.ItemId).FirstOrDefault();

                try
                {
                    PedidoItem PI = new PedidoItem();
                    PI.Item = itemTemp;
                    PI.ItemId = itemTemp.ItemId;
                    PI.Quantidade = 1;
                    PI.PedidoId = -1;

                    PedidoBase.Carrinho.Add(PI);

                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = item.ItemId });
                }
                catch (Exception ex)
                {
                    if (db.Items.Where(c => c.Nome.Equals(item.Nome)).Any())
                        ModelState.AddModelError(string.Empty, "Já existe um item com este nome.");
                    else
                        ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return Details(item.ItemId);
        }
    }
}