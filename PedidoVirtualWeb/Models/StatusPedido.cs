using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class StatusPedido
    {
        [Key]
        [Display(Name = "Código Status")]
        public int StatusPedidoId { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}