using PedidoVirtualWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Classes
{
    public class CombosHelper : IDisposable
    {
        private static PedidoVirtualContext db = new PedidoVirtualContext();

        public static List<Categoria> GetCategorias() {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria
            {
                CategoriaId = 0,
                Nome = "[ Selecione uma categoria ]"
            });

            return categorias.OrderBy(c => c.Nome).ToList();
        }

        public static List<Item> GetItems()
        {
            var items = db.Items.ToList();
            items.Add(new Item
            {
                ItemId = 0,
                Nome = "[ Selecione um item ]"
            });

            return items.OrderBy(i => i.Nome).ToList();
        }

        public static List<StatusPedido> GetStatus()
        {
            var status = db.StatusPedidos.ToList();
            status.Add(new StatusPedido
            {
                StatusPedidoId = 0,
                Nome = "[ Selecione um status ]"
            });

            return status.OrderBy(i => i.StatusPedidoId).ToList();
        }

        public static List<Usuario> GetUsuarios()
        {
            var usuarios = db.Usuarios.ToList();
            usuarios.Add(new Usuario
            {
                UsuarioId = 0,
                Nome = "[ Selecione um usuário ]"
            });

            return usuarios.OrderBy(i => i.NomeCompleto).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}