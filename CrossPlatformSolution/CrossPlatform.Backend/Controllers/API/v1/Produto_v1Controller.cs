using CrossPlatform.Backend.Data;
using CrossPlatform.Backend.Data.Dtos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace CrossPlatform.Backend.Controllers.API.v1
{
    [Authorize]
    [RoutePrefix("api/v1/produtos")]
    public class Produto_v1Controller : Base_v1Controller
    {
        private readonly ApplicationDbContext _Context;

        public Produto_v1Controller()
        {
            this._Context = new ApplicationDbContext();
        }

        // GET: api/v1/produtos
        [Route()]
        public async Task<JsonResult<List<ProdutoDto>>> Get()
        {
            return Json(await this._Context.Produtos.ToListAsync());
        }

        // GET: api/v1/produtos
        [Route()]
        public async Task<ProdutoDto> Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await this._Context.Produtos.FirstOrDefaultAsync(lbda => lbda.Id == id);
        }

        // POST: api/v1/produtos
        [Route()]
        public async Task<IHttpActionResult> Post([FromBody]ProdutoDto pDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                    return BadRequest(this.ModelState);

                if (pDto.Id == Guid.Empty)
                    pDto.Id = Guid.NewGuid(); // TODO: IMPLEMENTAR GUID SEQUENCIAL...

                var _dto = await this._Context.Produtos.FirstOrDefaultAsync(lbda => lbda.Id == pDto.Id);

                if (_dto == null)
                {
                    _dto = new ProdutoDto();
                    _dto.Id = pDto.Id;

                    pDto.DataInclusao = DateTime.UtcNow;
                    pDto.UsuarioInclusaoId = this.RequestContext.Principal.Identity.GetUserId();

                    this.ModelToDatabase(pDto, _dto);

                    this._Context.Produtos.Add(_dto);
                }
                else
                {
                    pDto.DataUltimaAlteracao = DateTime.UtcNow;
                    pDto.UsuarioAlteracaoId = this.RequestContext.Principal.Identity.GetUserId();

                    this.ModelToDatabase(pDto, _dto);
                }

                if (this._Context.ChangeTracker.HasChanges())
                    await this._Context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/v1/produtos
        [Route()]
        public void Put(Guid id, [FromBody]ProdutoDto value)
        {
        }

        // DELETE: api/v1/produtos/{id}
        [Route()]
        public void Delete(Guid id)
        {
        }

        private void ModelToDatabase(ProdutoDto pModel, ProdutoDto pDatabaseEntity)
        {
            pDatabaseEntity.Nome = pModel.Nome; //Nome -> NOT NULL
            pDatabaseEntity.Descricao = pModel.Descricao; //Descricao -> NULL
            pDatabaseEntity.CodigoBarra = pModel.CodigoBarra; //CodigoBarra -> NULL
            pDatabaseEntity.Ativo = pModel.Ativo; //Ativo -> NOT NULL
            pDatabaseEntity.ValorUnitario = pModel.ValorUnitario; //ValorUnitario -> NOT NULL
            pDatabaseEntity.DataInclusao = pModel.DataInclusao; //DataInclusao -> NOT NULL
            pDatabaseEntity.DataUltimaAlteracao = pModel.DataUltimaAlteracao; //DataUltimaAlteracao -> NULL
            pDatabaseEntity.UsuarioInclusaoId = pModel.UsuarioInclusaoId; //UsuarioInclusaoId -> NOT NULL
            pDatabaseEntity.UsuarioAlteracaoId = pModel.UsuarioAlteracaoId; //UsuarioAlteracaoId -> NULL
        }
    }
}
