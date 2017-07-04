using System;
using System.Net.Http;
using System.Threading.Tasks;
using Examples;

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
            using (var iReportsLibrary = await MakeAuthenticatedClient.DoWork("https://www.ireportsdevelopment.com/Test/api/v1", "Seth", "seth1612!"))
            {
                await SearchProduct.GetProducts(iReportsLibrary, 0, 10);
            }

        }
    }
}
