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
                "http://localhost:63352/api/v1/", "paulpopat", "paul123"))
            {
                await ShareSavedSearch.DoWork(iReportsLibrary, "paulpopat", "test");
                await GetPageOfProductNames.GetProducts(iReportsLibrary, 0, 10);
                await GetProductAttachmentsAndPrintCopies.DoWork(iReportsLibrary);
            }
        }
    }
}
