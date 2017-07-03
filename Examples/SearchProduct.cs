using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;

namespace Examples
{
    public class ProductModel
    {
        public string ProductCode { get; set; }

        public string Title { get; set; }

        public string UILink { get; set; }

        public string ApiLink { get; set; }

        public bool Licensed { get; set; }
    }

    public class ProductsModel
    {
        public List<ProductModel> Products { get; set; }

        public int ProductCount { get; set; }
    }

    public class SearchProduct
    {
        public static async Task<string> GetProducts(HttpClient client, int skip, int take)
        {
            var query = new Dictionary<string, string>
            {
                ["Skip"] = skip.ToString(),
                ["Take"] = take.ToString(),
                ["IncludeUnlicensed"] = "false",
                ["IncludePrivate"] = "true",
                ["IncludeComingSoon"] = "true",
                ["IncludeArchived"] = "true",
                ["IncludeDisabled"] = "false"
            };

            string queryString;
            using (var content = new FormUrlEncodedContent(query))
            {
                queryString = await content.ReadAsStringAsync();
            }

            var response = await client.GetAsync($"api/v1/products?{queryString}");
            string jsonString = await response.Content.ReadAsStringAsync();

            PrintTitlesFromJson(jsonString);

            return null;
        }

        private static void PrintTitlesFromJson(string jsonString)
        {
            var productsModel = JsonConvert.DeserializeObject<ProductsModel>(jsonString);

            foreach (var product in productsModel.Products)
            {
                Console.WriteLine($"Title = {product.Title}");
            }
        }
    }
}