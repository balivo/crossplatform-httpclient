using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Clients.Data.Dtos
{
    public abstract class DtoBase<TDtoBase, TPrimaryKey> where TDtoBase : DtoBase<TDtoBase, TPrimaryKey>
    {
        public abstract TPrimaryKey Id { get; set; }
    }
}
