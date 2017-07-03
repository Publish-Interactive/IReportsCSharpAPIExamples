using System;
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
            using (var client = await MakeAuthenticatedClient.DoWork("https://www.ireportsdevelopment.com/Test/", "Seth", "seth1612!"))
            {
                await SearchProduct.GetProducts(client, 0, 10);
            }

        }
    }
}
