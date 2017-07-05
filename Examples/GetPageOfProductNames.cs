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
    public class GetPageOfProductNames
    {
        public static async Task<ProductSearchResults> GetProducts(
            IReportsLibrary iReportsLibrary, int skip, int take)
        {
            ProductSearchResults productResults =
            await iReportsLibrary.GetProductsAsync(
                null,
                null,
                null,
                null,
                skip,
                take,
                false,
                true,
                true,
                true,
                false,
                null,
                null);

            foreach (var product in productResults.Products)
            {
                Console.WriteLine($"Title = {product.Title}");
            }

            return productResults;
        }
    }
}