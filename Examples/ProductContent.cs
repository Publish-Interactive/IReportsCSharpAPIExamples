using System;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class ProductContent
    {
        public static async Task DoWork(IReportsLibrary iReportsLibrary)
        {
            await GetAttachment(iReportsLibrary, "PRODUCT_CODE", "FILE_CODE");
            await GetContent(iReportsLibrary, "PRODUCT_CODE");
            await GetToc(iReportsLibrary, "PRODUCT_CODE");

            await GetPrintCopiesByExtension(iReportsLibrary, "PRODUCT_CODE", "FILE_EXTENSION");
        }

        public static async Task<ProductTocModel> GetToc(
            IReportsLibrary iReportsLibrary, string productCode)
        {
            ProductTocModel tocModel = await iReportsLibrary.GetProductTocAsync(
                "reports", productCode);

            Console.WriteLine($"First Chapter Summary = {tocModel.Chapters[0].Summary}");

            return tocModel;
        }

        public static async Task<FileDownloadModel> GetAttachment(
            IReportsLibrary iReportsLibrary, string productCode, string fileCode)
        {
            FileDownloadModel attachmentModel = await iReportsLibrary.GetAttachmentAsync(
                "reports", productCode, fileCode);

            Console.WriteLine($"File name = {attachmentModel.Name}");

            return attachmentModel;
        }

        public static async Task<ProductContentModel> GetContent(
            IReportsLibrary iReportsLibrary, string productCode)
        {
            ProductContentModel contentModel = await iReportsLibrary.GetContentAsync(
                "reports", productCode);

            Console.WriteLine($"Name of content = {contentModel.Content.Name}");

            return contentModel;
        }

        public static async Task<FileDownloadModel> GetPrintCopiesByExtension(
            IReportsLibrary iReportsLibrary, string productCode, string extension)
        {
            FileDownloadModel fileModel = await iReportsLibrary.GetPrintCopyAsync(
                "reports", productCode, extension, null);

            Console.WriteLine($"File Name = {fileModel.Name}");

            return fileModel;
        }

    }
}