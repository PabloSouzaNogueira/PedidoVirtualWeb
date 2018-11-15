using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidoVirtualWeb.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Código Usuário")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Sobrenome { get; set; }

        [Display(Name = "Usuário")]
        public string NomeCompleto { get { return string.Format("{0} {1}", Nome, Sobrenome); } }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Index("Usuario_Email_Index", IsUnique = true)]
        [MaxLength(50, ErrorMessage = "O campo {0} recebe no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Endereco { get; set; }


        public virtual ICollection<Comentario> Comentarios { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}