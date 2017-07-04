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
    public class SearchProduct
    {
        public static async Task GetProducts(IReportsLibrary iReportsLibrary, int skip, int take)
        {
            ProductSearchResults result = await iReportsLibrary.GetProductsAsync(
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

            foreach (var product in result.Products)
            {
                Console.WriteLine($"Title = {product.Title}");
            }
        }
    }
}