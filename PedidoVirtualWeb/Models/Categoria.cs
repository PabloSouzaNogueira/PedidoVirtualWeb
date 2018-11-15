using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Código Categoria")]
        public int CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        [Index("Categoria_Nome_Index", IsUnique = true)]
        [MaxLength(50, ErrorMessage = "O campo {0} recebe no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Item> Itens { get; set;}
    }
}