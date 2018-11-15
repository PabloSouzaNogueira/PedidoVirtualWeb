using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class Pedido
    {

        [Key]
        [Display(Name = "Código Pedido")]
        public int PedidoId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int UsuarioId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int StatusPedidoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }


        public virtual Usuario Usuario { get; set; }
        public virtual StatusPedido Status { get; set; }

        public virtual ICollection<PedidoItem> PedidoItems { get; set; }
    }
}