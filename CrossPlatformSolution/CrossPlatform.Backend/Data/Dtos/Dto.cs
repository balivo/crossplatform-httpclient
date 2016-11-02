using CrossPlatform.Backend.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossPlatform.Backend.Data.Dtos
{
    public abstract class Dto<TDto, TPrimaryKey> : DtoBase<TDto, TPrimaryKey> where TDto : Dto<TDto, TPrimaryKey>
    {
        [Required]
        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltimaAlteracao { get; set; }

        [Required]
        public string UsuarioInclusaoId { get; set; }

        [ForeignKey("UsuarioInclusaoId")]
        public ApplicationUser UsuarioInclusao { get; set; }

        public string UsuarioAlteracaoId { get; set; }

        [ForeignKey("UsuarioAlteracaoId")]
        public ApplicationUser UsuarioAlteracao { get; set; }
    }
}