using System;
using System.Net.Http;
using System.Threading.Tasks;
using Examples;
using IReportsApiExamples.Examples;

namespace IReportsApiExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            using (var iReportsLibrary = await MakeAuthenticatedClient.DoWork(
                "BASE_URL", "USERNAME", "PASSWORD"))
            {
                await GetProduct.GetProducts(iReportsLibrary, 0, 10);
                await ProductContent.DoWork(iReportsLibrary);
            }
        }
    }
}
