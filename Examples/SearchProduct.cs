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
        public static async Task<ProductSearchResults> GetProducts(IReportsLibrary iReportsLibrary, int skip, int take)
        {
            ProductSearchResults productResults = await iReportsLibrary.GetProductsAsync(
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

        public static async Task<ProductTocModel> GetProductToc(IReportsLibrary iReportsLibrary, string productCode)
        {
            ProductTocModel tocModel = await iReportsLibrary.GetProductTocAsync("reports", productCode);
            Console.WriteLine($"First Chapter Summary = {tocModel.Chapters[0].Summary}");
            
            return tocModel;
        }

        //TODO check error and push changes. then save Attachments to WORKING DIRECTORY. 

        public static async Task<FileDownloadModel> GetProductAttachment(IReportsLibrary iReportsLibrary, string productCode, string fileCode)
        {
            FileDownloadModel attachmentModel = await iReportsLibrary.GetAttachmentAsync("reports", productCode, fileCode);
            Console.WriteLine($"File name = {attachmentModel.Name}");

            return attachmentModel;
        }

        public static async Task<ProductContentModel> GetProductContent(IReportsLibrary iReportsLibrary, string productCode){
            ProductContentModel contentModel = await iReportsLibrary.GetContentAsync("reports", productCode);
            Console.WriteLine($"Name of content = {contentModel.Content.Name}");

            return contentModel;
        }

        public static async Task<FileDownloadModel> GetPrintCopiesByExtension(IReportsLibrary iReportsLibrary, string productCode, string extension){
            FileDownloadModel fileModel = await iReportsLibrary.GetPrintCopyAsync("reports", productCode, extension, null);
            Console.WriteLine($"File Name = {fileModel.Name}");

            return fileModel;
        }

    }
}