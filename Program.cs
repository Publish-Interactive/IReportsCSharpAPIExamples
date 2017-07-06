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
                "BASE_URL/api/v1/", "USERNAME", "PASSWORD"))
            {
                await ShareSavedSearch.DoWork(iReportsLibrary, "USERNAME", "SEARCH_TERMS");
                await GetPageOfProductNames.GetProducts(iReportsLibrary, 0, 10);
                await GetProductAttachmentsAndPrintCopies.DoWork(iReportsLibrary, "PRODUCT_CODE");
            }
        }
    }
}
