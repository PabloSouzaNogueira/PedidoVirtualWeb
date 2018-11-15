using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class PedidoItem
    {
        [Key, Column("PedidoId", Order = 0)]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int PedidoId { get; set; }

        [Key, Column("ItemId", Order = 1)]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int ItemId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Informe a {0}.")]
        public int Quantidade { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Item Item { get; set; }
    }
}