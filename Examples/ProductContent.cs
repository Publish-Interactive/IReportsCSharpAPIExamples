using System;
using System.IO;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class ProductContent
    {
        public static async Task DoWork(IReportsLibrary iReportsLibrary)
        {
            FileDownloadModel fileModel = await GetAttachment(iReportsLibrary, "PRODUCT_CODE", "FILE_CODE");
            SaveAttachmentToWorkingDirectory(fileModel);

            await GetContent(iReportsLibrary, "PRODUCT_CODE");
            await GetToc(iReportsLibrary, "PRODUCT_CODE");
            await GetPrintCopiesByExtension(iReportsLibrary, "PRODUCT_CODE", "FILE_EXTENSION");
        }

        /// <summary>Get the table of contents for a specified product</summary>
        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="productCode">The code of the desired product to get the table of content</param>
        public static async Task<ProductTocModel> GetToc(
            IReportsLibrary iReportsLibrary, string productCode)
        {
            ProductTocModel tocModel = await iReportsLibrary.GetProductTocAsync(
                "reports", productCode);

            Console.WriteLine($"First Chapter Summary = {tocModel.Chapters[0].Summary}");

            return tocModel;
        }

        /// <summary>Get an attachment from a specified product as a FileDownloadModel</summary>
        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="productCode">The code of the desired product to get the attachment</param>
        /// <param name="fileCode">The file code (name) of the desired attachment</param>
        public static async Task<FileDownloadModel> GetAttachment(
            IReportsLibrary iReportsLibrary, string productCode, string fileCode)
        {
            FileDownloadModel attachmentModel = await iReportsLibrary.GetAttachmentAsync(
                "reports", productCode, fileCode);

            Console.WriteLine($"File name = {attachmentModel.Name}");

            return attachmentModel;
        }

        /// <summary>save a specified attachment to the working directory</summary>
        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="fileModel">The model which contains the Name and Content of the desired attachment</param>
        public static void SaveAttachmentToWorkingDirectory(FileDownloadModel fileModel)
        {
            File.WriteAllBytes(fileModel.Name, fileModel.Content);
        }

        /// <summary>get the content of a specified product</summary>
        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="productCode">the code of the desired product to get the contents from</param>
        public static async Task<ProductContentModel> GetContent(
            IReportsLibrary iReportsLibrary, string productCode)
        {
            ProductContentModel contentModel = await iReportsLibrary.GetContentAsync(
                "reports", productCode);

            Console.WriteLine($"Name of content = {contentModel.Content.Name}");

            return contentModel;
        }

        /// <summary>gets a print copy with a specified extension</summary>
        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="productCode">the product code of the desired product to print copy</param>
        /// <param name="extension">the desired extension of the print copy</param>
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