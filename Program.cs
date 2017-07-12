using System.Threading.Tasks;
using Examples;
using IReportsApiExamples.Examples;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IReportsApiExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeEnumsToStringByDefault();
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            using (var wrapper = await MakeAuthenticatedClient.DoWork(
                "BASE_URL/api/v1/", "USERNAME", "PASSWORD"))
            {
                await ImportCategoriesAndProducts.DoWork(                   
                    wrapper,
                   "CATEGORIES_FILE_PATH",
                   "PRODUCTS_FILE_PATH",
                   "LIBRARY_CODE"
               );

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

        ///<summary>Automatically serialize Enums as strings by changing the global setting for Json.net<summary>
        private static void SerializeEnumsToStringByDefault()
        {
            JsonConvert.DefaultSettings = () =>
                        {
                            var settings = new JsonSerializerSettings();
                            settings.Converters.Add(new StringEnumConverter());
                            return settings;
                        };
        }
    }
}
