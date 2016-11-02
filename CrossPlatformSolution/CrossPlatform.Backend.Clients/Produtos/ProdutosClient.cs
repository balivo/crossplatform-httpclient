using CrossPlatform.Backend.Clients.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Clients.Produtos
{
    public static class ProdutosClient
    {
        public static Task<List<ProdutoDto>> Get()
        {
            return CrossPlatformHttpClientService
                .Current
                .GetAsync<List<ProdutoDto>>("api/v1/produtos");
        }
    }
}
