using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "Código Item")]
        public int ItemId { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione uma {0}.")]
        public int CategoriaId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }

        [Display(Name = "Item")]
        [Index("Item_Nome_Index", IsUnique = true)]
        [MaxLength(50, ErrorMessage = "O campo {0} recebe no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Descricao { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public double Preco { get; set; }

        [Display(Name = "Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "Informe a {0}.")]
        public int Estoque { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<PedidoItem> PedidoItems { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

        [NotMapped]
        [Display(Name = "Foto")]
        public HttpPostedFileBase FotoFile { get; set; }

        public string ComentarioTemporario { get; set; }

    }
}