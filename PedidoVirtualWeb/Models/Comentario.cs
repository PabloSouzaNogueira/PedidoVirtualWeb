using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class Comentario
    {
        [Key]
        [Display(Name = "Código Comentário")]
        public int ComentarioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int ItemId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Selecione um {0}.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        public virtual Item Item { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}