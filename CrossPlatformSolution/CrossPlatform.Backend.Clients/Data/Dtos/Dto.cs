using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Clients.Data.Dtos
{
    public abstract class Dto<TDto, TPrimaryKey> : DtoBase<TDto, TPrimaryKey> where TDto : Dto<TDto, TPrimaryKey>
    {
        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltimaAlteracao { get; set; }

        public string UsuarioInclusaoId { get; set; }

        public string UsuarioAlteracaoId { get; set; }
    }
}
