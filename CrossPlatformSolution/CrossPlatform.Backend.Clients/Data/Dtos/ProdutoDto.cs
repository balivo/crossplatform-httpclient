using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Clients.Data.Dtos
{
    public sealed class ProdutoDto : Dto<ProdutoDto, Guid>
    {
        public override Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoBarra { get; set; }
        public bool Ativo { get; set; } = true;
        public decimal ValorUnitario { get; set; }
    }
}
