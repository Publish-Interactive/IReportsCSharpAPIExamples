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
            using (var wrapper = await MakeAuthenticatedClient.DoWork(
                "BASE_URL/api/v1/", "USERNAME", "PASSWORD"))
            {
                ImportAccountsAndSubscribers.DoWork(
                    wrapper,
                    "ACCOUNTS_FILE_PATH",
                    "SUBSCRIBERS_FILE_PATH");

                await MoveUserToAnotherAccount.DoWork(wrapper,
                    "USERNAME",
                    "NEW_ACCOUNT_NAME",
                    "COMPANY_NAME",
                    "COUNTRY_CODE",
                    "EMAIL_ADDRESS");

                await ShareSavedSearch.DoWork(wrapper, "USERNAME", "SEARCH_TERMS");
                await GetPageOfProductNames.GetProducts(wrapper, 0, 10);
                await GetProductAttachmentsAndPrintCopies.DoWork(wrapper, "PRODUCT_CODE");

            }
        }
    }
}
