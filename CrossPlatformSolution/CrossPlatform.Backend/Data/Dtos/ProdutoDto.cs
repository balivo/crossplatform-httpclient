using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Data.Dtos
{
    [Table("Produto")]
    public sealed class ProdutoDto : Dto<ProdutoDto, Guid>
    {
        [Key]
        public override Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string CodigoBarra { get; set; }

        public bool Ativo { get; set; } = true;

        [Required]
        public decimal ValorUnitario { get; set; }
    }
}
