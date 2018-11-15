using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PedidoVirtualWeb.Models
{
    public class PedidoVirtualContext : DbContext
    {
        public PedidoVirtualContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }



        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.Pedido> Pedidos { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.StatusPedido> StatusPedidos { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.PedidoItem> PedidoItems { get; set; }

        public System.Data.Entity.DbSet<PedidoVirtualWeb.Models.Comentario> Comentarios { get; set; }
    }
}