using CrossPlatform.Backend.Clients;
using CrossPlatform.Backend.Clients.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(async () =>
            {
                await CrossPlatformHttpClientService.Current.Autenticar();

                var _result = await ProdutosClient.Get();

                foreach (var item in _result)
                {
                    System.Console.WriteLine(item.Nome);
                }
            });

            System.Console.ReadKey();
        }
    }
}
