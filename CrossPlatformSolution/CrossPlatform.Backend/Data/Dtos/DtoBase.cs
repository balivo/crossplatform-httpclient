using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrossPlatform.Backend.Data.Dtos
{
    public abstract class DtoBase<TDtoBase, TPrimaryKey> where TDtoBase : DtoBase<TDtoBase, TPrimaryKey>
    {
        public abstract TPrimaryKey Id { get; set; }

        // Configura automaticamente o ConcurrencyCheck and DatabaseGeneratedPatter=Computed.
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}